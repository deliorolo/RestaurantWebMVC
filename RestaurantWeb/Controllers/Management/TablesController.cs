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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IDataAccessSubCategory<ITableModel> tableData = Factory.InstanceTableDataAccess();
        private IDataAccessRegular<IAreaModel> areaData = Factory.InstanceAreaDataAccess();

        // Lists all tables
        public ActionResult Index()
        {
            try
            {
                return View(tableData.GetAll().OrderBy(x => x.NumberOfTable));
            }
            catch (Exception ex)
            {
                log.Error("Could't load tables from Database", ex);
                return View("ErrorRetriveData");
            }

        }

        // Form to create a new table
        public ActionResult Create()
        {
            try
            {
                ViewBag.AreaID = new SelectList(areaData.GetAll(), "ID", "Name");
            }
            catch (Exception ex)
            {
                log.Error("Could't load areas from Database", ex);
                return View("ErrorRetriveData");
            }
            return View();
        }

        // Adds a new table to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumberOfTable,AreaID,Occupied")] TableModel table)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Checking if already exist a table with same number
                    if (tableData.CheckIfAlreadyExist(table.NumberOfTable.ToString()) == false)
                    {
                        ITableModel model = table;
                        tableData.Create(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        log.Info("The user tried to add a table that already existed");
                        return View("AlreadyExists");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could't create a new table in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The model state of the table is invalid");
                return View("WrongData");
            }
        }

        // Asks for confirmation in order to delete a table from the database
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    ITableModel table = tableData.FindById((int)id);

                    if (table == null)
                    {
                        log.Error("Could't find a table in the Database - return null");
                        return View("ErrorDelete");
                    }

                    if (table.SoldProducts.Count > 0)
                    {
                        log.Info("The user tried to delete a table that was opened");
                        return View("OpenedTable");
                    }

                    return View(table);
                }
                catch (Exception ex)
                {
                    log.Error("Could't find a table in the Database", ex);
                    return View("ErrorRetriveData");
                } 
            }
            else
            {
                log.Error("The table ID was null while trying to delete");
                return View("ErrorDelete");
            }
        }

        // Deletes a table from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                tableData.Delete(id);
            }
            catch (Exception ex)
            {
                log.Error("Could't delete a table from the Database", ex);
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Index");
        }

        // Lists the tables that belong to a selected area
        public ActionResult TablesByArea(int? id)
        {
            if (id == null)
            {
                log.Error("The area ID was null while trying to load tables");
                return View("ErrorAccessArea");
            }

            try
            {
                return View(tableData.GetBySubGroup((int)id));
            }
            catch (Exception ex)
            {
                log.Error("Could't load tables of a area from Database", ex);
                return View("ErrorRetriveData");
            }
        }
    }
}
