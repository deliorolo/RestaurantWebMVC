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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IDataAccessSubCategory<IProductModel> productData = Factory.InstanceProductDataAccess();
        private IDataAccessRegular<ICategoryModel> categoryData = Factory.InstanceCategoryDataAccess();

        // Lists all products
        public ActionResult Index()
        {
            try
            {
                return View(productData.GetAll().OrderBy(x => x.Category.Name));
            }
            catch (Exception ex)
            {
                log.Error("Could't load products from Database", ex);
                return View("ErrorRetriveData");
            }           
        }

        // Form to create a new product
        public ActionResult Create()
        {
            try
            {
                ViewBag.CategoryID = new SelectList(categoryData.GetAll(), "ID", "Name");
            }
            catch (Exception ex)
            {
                log.Error("Could't load categories from Database", ex);
                return View("ErrorRetriveData");
            }
            return View();
        }

        // Adds a new product to the database
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
                        log.Info("The user tried to add a product that already existed");
                        return View("AlreadyExists");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could't create a new product in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The model state of the product is invalid");
                return View("WrongData");
            }
        }

        // Form to edit a product
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
                        log.Error("Could't find a product in the Database - return null");
                        return View("ErrorEdit");
                    }
                    return View(product);
                }

                catch (Exception ex)
                {
                    log.Error("Could't find a product in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The product ID was null while trying to edit");
                return View("ErrorEdit");
            }
        }

        // Changes an existing product in the database
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
                        log.Info("The user tried to add a product that already existed");
                        return View("AlreadyExists");
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Could't edit a product from Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The model state of the product is invalid");
                return View("ErrorEdit");
            }
        }

        // Asks for confirmation in order to delete a product from the database
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                try
                {
                    IProductModel product = productData.FindById((int)id);

                    if (product == null)
                    {
                        log.Error("Could't find a product in the Database - return null");
                        return View("ErrorDelete");
                    }

                    return View(product);
                }

                catch (Exception ex)
                {
                    log.Error("Could't find a product in the Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The product ID was null while trying to delete");
                return View("ErrorDelete");
            }
        }

        // Deletes a product from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                productData.Delete(id);
            }
            catch (Exception ex)
            {
                log.Error("Could't delete a product from the Database", ex);
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Index");
        }

        // Lists the products that belong to a selected category
        public ActionResult ProductsByCategory(int? id)
        {
            if (id == null)
            {
                log.Error("The category ID was null while trying to load products");
                return View("ErrorAccessCategory");
            }

            try
            {
                return View(productData.GetBySubGroup((int)id));
            }
            catch (Exception ex)
            {
                log.Error("Could't load products of a category from Database", ex);
                return View("ErrorRetriveData");
            }
        }

    }
}
