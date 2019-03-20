using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantWeb.Models;
using RestaurantWeb.Services;

namespace RestaurantWeb.Controllers
{
    public class TablesController : Controller
    {
        private IDataAccessSubCategory<ITableModel> tableData = new TableDataAccess();
        private IDataAccessRegular<IAreaModel> areaData = new AreaDataAccess();

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

        public ActionResult Edit(int? id)
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

            ViewBag.AreaID = new SelectList(areaData.GetAll(), "ID", "Name");

            return View(table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NumberOfTable,AreaID,Occupied")] TableModel table)
        {
            if (ModelState.IsValid)
            {
                if (tableData.CheckIfAlreadyExist(table.NumberOfTable.ToString()) == false ||
                    tableData.FindById(table.ID).NumberOfTable == table.NumberOfTable)
                {
                    ITableModel model = table;
                    tableData.Update(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("AlreadyExists");
                }
            }
            return View(table);
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
