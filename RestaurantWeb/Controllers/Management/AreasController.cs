using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestaurantWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AreasController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IDataAccessRegular<IAreaModel> areaData = Factory.InstanceAreaDataAccess();
        private IDataAccessSubCategory<ITableModel> tableData = Factory.InstanceTableDataAccess();

        // Lists all areas
        public ActionResult Index()
        {
            try
            {
                return View(areaData.GetAll());
            }
            catch (Exception ex)
            {
                log.Error("Could't load areas from Database", ex);
                return View("ErrorRetriveData");
            }
        }

        // Form to create a new area
        public ActionResult Create()
        {
            return View();
        }

        // Adds a new area to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] AreaModel area)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Checking if already exist an area with same name (case insensitive)
                    if (areaData.CheckIfAlreadyExist(area.Name) == false)
                    {
                        IAreaModel model = area;
                        areaData.Create(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        log.Info("The user tried to add an area that already existed");
                        return View("AlreadyExists");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could't create a new area in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The model state of the area is invalid");
                return View("WrongData");
            }
        }

        // Form to edit an area
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                try
                {
                    IAreaModel area = areaData.FindById((int)id);

                    if (area == null)
                    {
                        log.Error("Could't find an area in the Database - return null");
                        return View("ErrorEdit");
                    }
                    return View(area);
                }

                catch (Exception ex)
                {
                    log.Error("Could't find an area in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The area ID was null while trying to edit");
                return View("ErrorEdit");
            }
        }

        // Changes an existing area in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] AreaModel area)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Checking if already exist an area with same name (case insensitive)
                    if (areaData.CheckIfAlreadyExist(area.Name) == false)
                    {
                        IAreaModel model = area;
                        areaData.Update(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        log.Info("The user tried to add an area that already existed");
                        return View("AlreadyExists");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could't edit an area from Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The model state of the area is invalid");
                return View("ErrorEdit");
            }
        }

        // Asks for confirmation in order to delete an area from the database
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    IAreaModel area = areaData.FindById((int)id);

                    if (area == null)
                    {
                        log.Error("Could't find an area in the Database - return null");
                        return View("ErrorDelete");
                    }

                    // It gets the number of tables in selected area in order to display to the user
                    List<ITableModel> tables = tableData.GetBySubGroup((int)id);
                    area.NumberOfTables = tables.Count();

                    foreach (ITableModel table in tables)
                    {
                        if (table.Occupied)
                        {
                            log.Info("The user tried to delete an area with opened tables");
                            return View("OpenedTable");
                        }
                    }

                    return View(area);
                }

                catch (Exception ex)
                {
                    log.Error("Could't find an area in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The area ID was null while trying to delete");
                return View("ErrorDelete");
            }
        }

        // Deletes an area from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                areaData.Delete(id);
            }
            catch (Exception ex)
            {
                log.Error("Could't delete an area from the Database", ex);
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Index");
        }
    }
}
