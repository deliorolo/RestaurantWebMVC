using System;
using System.Linq;
using System.Web.Mvc;
using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;

namespace RestaurantWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            catch (Exception ex)
            {
                log.Error("Could't load categories from Database", ex);
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
                        log.Info("The user tried to add a category that already existed");
                        return View("AlreadyExists");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could't create a new category in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The model state of the category is invalid");
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
                        log.Error("Could't find a category in the Database - return null");
                        return View("ErrorEdit");
                    }
                    return View(category);
                }

                catch (Exception ex)
                {
                    log.Error("Could't find a category in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The category ID was null while trying to edit");
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
                        log.Info("The user tried to add a category that already existed");
                        return View("AlreadyExists");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could't edit a category from Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The model state of the category is invalid");
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
                        log.Error("Could't find a category in the Database - return null");
                        return View("ErrorDelete");
                    }

                    if (soldProductData.GetByCategory((int)id).Count() > 0 ||
                        soldProductAccomplishedData.GetByCategory((int)id).Count > 0)
                    {
                        log.Info("The user tried to delete a category with products in opened tables");
                        return View("OpenedTables");
                    }

                    category.NumberOfProducts = productData.GetBySubGroup((int)id).Count();

                    return View(category);
                }
                catch (Exception ex)
                {
                    log.Error("Could't find a category in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The category ID was null while trying to delete");
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
            catch (Exception ex)
            {
                log.Error("Could't delete a category from the Database", ex);
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Index");
        }
    }
}
