using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework.DataAccess;
using CodeLibrary.ModelsMVC;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
//using RestaurantWeb.AccessoryCode;
//using RestaurantWeb.Models;
//using RestaurantWeb.InternalServices;

namespace RestaurantWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AreasController : Controller
    {
        private IDataAccessRegular<IAreaModel> areaData = Factory.InstanceAreaDataAccess();
        private IDataAccessSubCategory<ITableModel> tableData = Factory.InstanceTableDataAccess();

        public ActionResult Index()
        {
            return View(areaData.GetAll());
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
                if (areaData.CheckIfAlreadyExist(area.Name) == false)
                {
                    IAreaModel model = area;
                    areaData.Create(model);
                }
                else
                {
                    return View("AlreadyExists");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IAreaModel area = areaData.FindById((int)id);

            if (area == null)
            {
                return HttpNotFound();
            }

            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] AreaModel area)
        {
            if (ModelState.IsValid)
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
            return View(area);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IAreaModel area = areaData.FindById((int)id);

            if (area == null)
            {
                return HttpNotFound();
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            areaData.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
