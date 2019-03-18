﻿using System;
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
    public class AreasController : Controller
    {
        private IDataAccessRegular<IAreaModel> areaData = new AreaDataAccess();

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
                IAreaModel model = area;
                areaData.Create(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IAreaModel area = areaData.Get((int)id);

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
                IAreaModel model = area;
                areaData.Update(model);
                return RedirectToAction("Index");
            }
            return View(area);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IAreaModel area = areaData.Get((int)id);

            if (area == null)
            {
                return HttpNotFound();
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
