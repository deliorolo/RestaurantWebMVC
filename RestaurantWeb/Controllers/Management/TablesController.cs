using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using RestaurantWeb.Services;

namespace RestaurantWeb.Controllers
{
    public class TablesController : Controller
    {
        private IDataAccessSubCategory<TableModel> tableData = new TableDataAccess();
        private IDataAccessRegular<AreaModel> areaData = new AreaDataAccess();

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
                tableData.Create(table);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TableModel table = tableData.Get((int)id);

            if (table == null)
            {
                return HttpNotFound();
            }

            ViewBag.AreaID = new SelectList(areaData.GetAll(), "ID", "Name");

            return View(table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NumberOfTable,AreaID,Occupied")] TableModel table)
        {
            if (ModelState.IsValid)
            {
                tableData.Update(table);
                return RedirectToAction("Index");
            }
            return View(table);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TableModel table = tableData.Get((int)id);

            if (table == null)
            {
                return HttpNotFound();
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

            return View(tableData.GetByParameter((int)id));
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
