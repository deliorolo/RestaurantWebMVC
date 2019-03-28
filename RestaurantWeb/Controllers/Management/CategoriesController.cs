using System.Linq;
using System.Net;
using System.Web.Mvc;
//using RestaurantWeb.AccessoryCode;
//using RestaurantWeb.Models;
//using RestaurantWeb.InternalServices;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework.DataAccess;
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
            
            if (category == null)
            {
                return HttpNotFound();
            }

            if(soldProductData.GetByCategory((int)id).Count() > 0 ||
                soldProductAccomplishedData.GetByCategory((int)id).Count > 0)
            {
                return View("OpenedTables");
            }

            category.NumberOfProducts = productData.GetBySubGroup((int)id).Count();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryData.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
