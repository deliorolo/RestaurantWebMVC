using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace RestaurantWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AreasController : Controller
    {
        private IDataAccessRegular<IAreaModel> areaData = Factory.InstanceAreaDataAccess();
        private IDataAccessSubCategory<ITableModel> tableData = Factory.InstanceTableDataAccess();

        public ActionResult Index()
        {
            try
            {
                return View(areaData.GetAll());
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] AreaModel area)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (areaData.CheckIfAlreadyExist(area.Name) == false)
                    {
                        IAreaModel model = area;
                        areaData.Create(model);
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

        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                try
                {
                    IAreaModel area = areaData.FindById((int)id);

                    if (area == null)
                    {
                        return View("ErrorEdit");
                    }
                    return View(area);
                }

                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                return View("ErrorEdit");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] AreaModel area)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (areaData.CheckIfAlreadyExist(area.Name) == false)
                    {
                        IAreaModel model = area;
                        areaData.Update(model);
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
                return View("ErrorEdit");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    IAreaModel area = areaData.FindById((int)id);

                    if (area == null)
                    {
                        return View("ErrorDelete");
                    }

                    List<ITableModel> tables = tableData.GetBySubGroup((int)id);
                    area.NumberOfTables = tables.Count();

                    foreach (ITableModel table in tables)
                    {
                        if (table.Occupied)
                        {
                            return View("OpenedTable");
                        }
                    }

                    return View(area);
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
                areaData.Delete(id);
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Index");
        }
    }
}
