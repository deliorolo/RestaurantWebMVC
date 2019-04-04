using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;

namespace RestaurantWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class TablesController : Controller
    {
        private IDataAccessSubCategory<ITableModel> tableData = Factory.InstanceTableDataAccess();
        private IDataAccessRegular<IAreaModel> areaData = Factory.InstanceAreaDataAccess();

        public ActionResult Index()
        {
            try
            {
                return View(tableData.GetAll().OrderBy(x => x.NumberOfTable));
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }

        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.AreaID = new SelectList(areaData.GetAll(), "ID", "Name");
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumberOfTable,AreaID,Occupied")] TableModel table)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (tableData.CheckIfAlreadyExist(table.NumberOfTable.ToString()) == false)
                    {
                        ITableModel model = table;
                        tableData.Create(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View("AlreadyExists");
                    }
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                return View("WrongData");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    ITableModel table = tableData.FindById((int)id);

                    if (table == null)
                    {
                        return View("ErrorDelete");
                    }

                    if (table.SoldProducts.Count > 0)
                    {
                        return View("OpenedTable");
                    }

                    return View(table);
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                } 
            }
            else
            {
                return View("ErrorDelete");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                tableData.Delete(id);
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Index");
        }

        public ActionResult TablesByArea(int? id)
        {
            if (id == null)
            {
                return View("ErrorAccessArea");
            }

            try
            {
                return View(tableData.GetBySubGroup((int)id));
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }
        }
    }
}
