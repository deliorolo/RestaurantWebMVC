using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantWeb.AccessoryCode;
using RestaurantWeb.Models;
using RestaurantWeb.InternalServices;

namespace RestaurantWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class TablesController : Controller
    {
        private IDataAccessSubCategory<ITableModel> tableData = ObjectCreator.TableDataAccess();
        private IDataAccessRegular<IAreaModel> areaData = ObjectCreator.AreaDataAccess();

        public ActionResult Index()
        {
            return View(tableData.GetAll().OrderBy(x => x.NumberOfTable));
        }

        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(areaData.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumberOfTable,AreaID,Occupied")] TableModel table)
        {
            if (ModelState.IsValid)
            {
                if (tableData.CheckIfAlreadyExist(table.NumberOfTable.ToString()) == false)
                {
                    ITableModel model = table;
                    tableData.Create(model);
                }
                else
                {
                    return View("AlreadyExists");
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ITableModel table = tableData.FindById((int)id);

            if (table == null)
            {
                return HttpNotFound();
            }

            if(table.SoldProducts.Count > 0)
            {
                return View("OpenedTable");
            }

            return View(table);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tableData.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult TablesByArea(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(tableData.GetBySubGroup((int)id));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
