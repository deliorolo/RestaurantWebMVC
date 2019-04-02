using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;

namespace RestaurantWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private IDataAccessRegular<ICategoryModel> categoryData = Factory.InstanceCategoryDataAccess();
        private IDataAccessSubCategory<IProductModel> productData = Factory.InstanceProductDataAccess();
        private ISoldProductDataAccess soldProductData = Factory.InstanceSoldProductDataAccess();
        private ISoldProductAccomplishedDataAccess soldProductAccomplishedData = Factory.InstanceSoldProductAccomplishedDataAccess();

        public ActionResult Index()
        {
            try
            {
                return View(categoryData.GetAll());
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
        public ActionResult Create([Bind(Include = "ID,Name")] CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (categoryData.CheckIfAlreadyExist(category.Name) == false)
                    {
                        ICategoryModel model = category;
                        categoryData.Create(model);
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
                    ICategoryModel category = categoryData.FindById((int)id);

                    if (category == null)
                    {
                        return View("ErrorEdit");
                    }
                    return View(category);
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
        public ActionResult Edit([Bind(Include = "ID,Name")] CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                try
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
                    ICategoryModel category = categoryData.FindById((int)id);

                    if (category == null)
                    {
                        return View("ErrorDelete");
                    }

                    if (soldProductData.GetByCategory((int)id).Count() > 0 ||
                        soldProductAccomplishedData.GetByCategory((int)id).Count > 0)
                    {
                        return View("OpenedTables");
                    }

                    category.NumberOfProducts = productData.GetBySubGroup((int)id).Count();

                    return View(category);
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
                categoryData.Delete(id);
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Index");
        }
    }
}
