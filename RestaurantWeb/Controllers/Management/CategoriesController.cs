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
    public class CategoriesController : Controller
    {
        private IDataAccessRegular<ICategoryModel> categoryData = ObjectCreator.CategoryDataAccess();
        private IDataAccessSubCategory<IProductModel> productData = ObjectCreator.ProductDataAccess();

        public ActionResult Index()
        {
            return View(categoryData.GetAll());
        }
  
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                if (categoryData.CheckIfAlreadyExist(category.Name) == false)
                {
                    ICategoryModel model = category;
                    categoryData.Create(model);
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

            ICategoryModel category = categoryData.FindById((int)id);

            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                if (categoryData.CheckIfAlreadyExist(category.Name) == false)
                {
                    ICategoryModel model = category;
                    categoryData.Update(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("AlreadyExists");
                }
            }
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ICategoryModel category = categoryData.FindById((int)id);
            category.NumberOfProducts = productData.GetBySubGroup((int)id).Count();

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryData.Delete(id);

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
