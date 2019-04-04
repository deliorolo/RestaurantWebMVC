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
    public class ProductsController : Controller
    {
        private IDataAccessSubCategory<IProductModel> productData = Factory.InstanceProductDataAccess();
        private IDataAccessRegular<ICategoryModel> categoryData = Factory.InstanceCategoryDataAccess();

        public ActionResult Index()
        {
            try
            {
                return View(productData.GetAll().OrderBy(x => x.Category.Name));
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }           
        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.CategoryID = new SelectList(categoryData.GetAll(), "ID", "Name");
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID,Price")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (productData.CheckIfAlreadyExist(product.Name) == false)
                    {
                        IProductModel model = product;
                        productData.Create(model);
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
                    IProductModel product = productData.FindById((int)id);
                    ViewBag.CategoryID = new SelectList(categoryData.GetAll(), "ID", "Name", product.CategoryID);

                    if (product == null)
                    {
                        return View("ErrorEdit");
                    }
                    return View(product);
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
        public ActionResult Edit([Bind(Include = "ID,Name,CategoryID,Price")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                try
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
                    IProductModel product = productData.FindById((int)id);

                    if (product == null)
                    {
                        return View("ErrorDelete");
                    }

                    return View(product);
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
                productData.Delete(id);
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Index");
        }

        public ActionResult ProductsByCategory(int? id)
        {
            if (id == null)
            {
                return View("ErrorAccessCategory");
            }

            try
            {
                return View(productData.GetBySubGroup((int)id));
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }
        }

    }
}
