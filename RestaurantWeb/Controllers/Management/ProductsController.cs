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
    public class ProductsController : Controller
    {
        private IDataAccessSubCategory<IProductModel> productData = new ProductDataAccess();
        private IDataAccessRegular<ICategoryModel> categoryData = new CategoryDataAccess();

        public ActionResult Index()
        {
            return View(productData.GetAll().OrderBy(x => x.Category.Name));
        }

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(categoryData.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID,Price")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                if (productData.CheckIfAlreadyExist(product.Name) == false)
                {
                    IProductModel model = product;
                    productData.Create(model);
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

            IProductModel product = productData.FindById((int)id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryID = new SelectList(categoryData.GetAll(), "ID", "Name");

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryID,Price")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                if (productData.CheckIfAlreadyExist(product.Name) == false ||
                    productData.FindById(product.ID).Name == product.Name)
                {
                    IProductModel model = product;
                    productData.Update(model);
                    return RedirectToAction("Index"); 
                }
                else
                {
                    return View("AlreadyExists");
                }
            }
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IProductModel product = productData.FindById((int)id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productData.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult ProductsByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(productData.GetBySubGroup((int)id));
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
