using RestaurantWeb.Models;
using RestaurantWeb.InternalServices;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RestaurantWeb.AccessoryCode;

namespace RestaurantWeb.Controllers
{
    [Authorize]
    public class MainMenuController : Controller
    {
        private IDataAccessSubCategory<IProductModel> productData = Factory.InstanceProductDataAccess();
        private IDataAccessRegular<ICategoryModel> categoryData = Factory.InstanceCategoryDataAccess();
        private IDataAccessSubCategory<ITableModel> tableData = Factory.InstanceTableDataAccess();
        private IDataAccessRegular<IAreaModel> areaData = Factory.InstanceAreaDataAccess();
        private ISoldProductDataAccess soldProductData = Factory.InstanceSoldProductDataAccess();
        private ISoldProductAccomplishedDataAccess soldProductAccomplishedData = Factory.InstanceSoldProductAccomplishedDataAccess();

        private IMainPageModel mainPageModel = Factory.InstanceMainPageModel();

        public ActionResult Tables()
        {
            mainPageModel.Areas = areaData.GetAll();
            mainPageModel.Tables = tableData.GetAll();

            return View(mainPageModel);
        }

        public ActionResult TableCategories(int? id)
        {
            ITableModel table = Factory.InstanceTableModel();

            mainPageModel.Categories = categoryData.GetAll();
            table = tableData.FindById((int)id);
            mainPageModel.Tables.Add(table);

            return View(mainPageModel);
        }

        public ActionResult TableProducts(int? idTable, int? idCategory)
        {
            ITableModel table = Factory.InstanceTableModel();

            table = tableData.FindById((int)idTable);
            mainPageModel.Tables.Add(table);

            mainPageModel.Products = productData.GetBySubGroup((int)idCategory);

            return View(mainPageModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("TableProducts")]
        public ActionResult TableAddProduct(int? idTable, int? idCategory, int? idProduct)
        {
            ITableModel table = Factory.InstanceTableModel();
            IProductModel product = Factory.InstanceProductModel();           

            table = tableData.FindById((int)idTable);
            product = productData.FindById((int)idProduct);

            ISoldProductModel soldProduct = MappingObjects.ProductToSoldProduct(product, (int)idTable);

            soldProductData.Create(soldProduct);

            table.Occupied = true;
            tableData.Update(table);
            table.SoldProducts = soldProductData.GetByTable(table.ID);

            mainPageModel.Products = productData.GetBySubGroup((int)idCategory);
            mainPageModel.Tables.Add(table);

            return View(mainPageModel);
        }

        public ActionResult PayAll(int? id)
        {
            ITableModel table = Factory.InstanceTableModel();
            table = tableData.FindById((int)id);

            return View(table);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("PayAll")]
        public ActionResult PayAllConfirm(int? id)
        {
            List<ISoldProductModel> sold = soldProductData.GetByTable((int)id);

            MainMenuHelper.PaySoldProducts(sold, soldProductData, soldProductAccomplishedData);

            ITableModel table = tableData.FindById((int)id);
            table.Occupied = false;
            tableData.Update(table);

            return RedirectToAction("Tables");
        }

        public ActionResult PayPartial(int? id)
        {
            ITableModel table = Factory.InstanceTableModel();
            table = tableData.FindById((int)id);

            return View(table);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PayPartial(int? id, int[] Paid)
        {
            if (Paid != null)
            {
                List<ISoldProductModel> fullList = soldProductData.GetByTable((int)id);
                MainMenuHelper.PaySelectedSoldProducts(fullList, Paid, soldProductData, soldProductAccomplishedData);

                if (fullList.Count() < 1)
                {
                    ITableModel table = tableData.FindById((int)id);
                    table.Occupied = false;
                    tableData.Update(table);
                }
            }

            return RedirectToAction("Tables");
        }

        public ActionResult Delete(int? idItem)
        {
            if (idItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ISoldProductModel product = soldProductData.FindById((int)idItem);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idItem)
        {
            ISoldProductModel product = soldProductData.FindById(idItem);
            ITableModel table = tableData.GetAll().Where(x => x.ID == product.TableID).FirstOrDefault();

            if (table.SoldProducts.Count() == 1)
            {
                table.Occupied = false;
                tableData.Update(table);
            }

            soldProductData.Delete(idItem);

            return RedirectToAction("Tables");
        }

        public ActionResult Edit(int? idItem)
        {
            if (idItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ISoldProductModel product = soldProductData.FindById((int)idItem);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed([Bind(Include = "ID, Price, Detail")] SoldProductModel product)
        {
            if (ModelState.IsValid)
            {
                ISoldProductModel model = product;
                soldProductData.Update(model);
                return RedirectToAction("Tables");
            }

            return View(product);
        }

        //TODO list
        // verify all buttons
        // use sqlite
        // Change home controller views
        // Add Comments
        // check view bags
        // refactoring the code
        // add pages for errors
        // unit testing
    }
}