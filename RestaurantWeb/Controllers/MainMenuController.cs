using RestaurantWeb.Models;
using RestaurantWeb.InternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantWeb.AccessoryCode;

namespace RestaurantWeb.Controllers
{
    [Authorize]
    public class MainMenuController : Controller
    {
        private IDataAccessSubCategory<IProductModel> productData = ObjectCreator.ProductDataAccess();
        private IDataAccessRegular<ICategoryModel> categoryData = ObjectCreator.CategoryDataAccess();
        private IDataAccessSubCategory<ITableModel> tableData = ObjectCreator.TableDataAccess();
        private IDataAccessRegular<IAreaModel> areaData = ObjectCreator.AreaDataAccess();
        private ISoldProductDataAccess soldProductData = ObjectCreator.SoldProductDataAccess();
        private ISoldProductAccomplishedDataAccess soldProductAccomplishedData = ObjectCreator.SoldProductAccomplishedDataAccess();

        private IMainPageModel mainPageModel = ObjectCreator.MainPageModel();

        public ActionResult Tables()
        {
            mainPageModel.Areas = areaData.GetAll();
            mainPageModel.Tables = tableData.GetAll();

            return View(mainPageModel);
        }

        public ActionResult TableCategories(int? id)
        {
            ITableModel table = ObjectCreator.TableModel();
            mainPageModel.Tables = ObjectCreator.ITableModelList();

            mainPageModel.Categories = categoryData.GetAll();
            table = tableData.FindById((int)id);
            mainPageModel.Tables.Add(table);

            return View(mainPageModel);
        }

        public ActionResult TableProducts(int? idTable, int? idCategory)
        {
            ITableModel table = ObjectCreator.TableModel();
            mainPageModel.Tables = ObjectCreator.ITableModelList();

            table = tableData.FindById((int)idTable);
            mainPageModel.Tables.Add(table);

            mainPageModel.Products = productData.GetBySubGroup((int)idCategory);

            return View(mainPageModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("TableProducts")]
        public ActionResult TableAddProduct(int? idTable, int? idCategory, int? idProduct)
        {
            ITableModel table = ObjectCreator.TableModel();
            IProductModel product = ObjectCreator.ProductModel();           
            mainPageModel.Tables = ObjectCreator.ITableModelList();

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
            ITableModel table = ObjectCreator.TableModel();
            table = tableData.FindById((int)id);

            return View(table);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("PayAll")]
        public ActionResult PayAllConfirm(int? id)
        {
            List<ISoldProductModel> sold = soldProductData.GetByTable((int)id);

            foreach (ISoldProductModel product in sold)
            {
                ISoldProductAccomplishedModel auxProduct = MappingObjects.SoldProductToSoldProductAccomplished(product);
                soldProductAccomplishedData.Create(auxProduct);
            }

            foreach (ISoldProductModel item in sold)
            {
                soldProductData.Delete(item.ID);
            }

            ITableModel table = tableData.FindById((int)id);
            table.Occupied = false;
            tableData.Update(table);

            return RedirectToAction("Tables");
        }

        public ActionResult PayPartial(int? id)
        {
            ITableModel table = ObjectCreator.TableModel();
            table = tableData.FindById((int)id);

            return View(table);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PayPartial(int? id, int[] Paid)
        {
            if (Paid != null)
            {
                List<ISoldProductModel> remaining = soldProductData.GetByTable((int)id);
                List<ISoldProductModel> sold = ObjectCreator.ISoldProductModelList();

                foreach (int item in Paid)
                {
                    ISoldProductModel product = ObjectCreator.SoldProductModel();
                    product = remaining[item];

                    ISoldProductAccomplishedModel auxProduct = MappingObjects.SoldProductToSoldProductAccomplished(product);
                    
                    soldProductAccomplishedData.Create(auxProduct);
                    sold.Add(product);
                }

                foreach (ISoldProductModel element in sold)
                {
                    remaining.Remove(element);
                }

                if (remaining.Count() < 1)
                {
                    ITableModel table = tableData.FindById((int)id);
                    table.Occupied = false;
                    tableData.Update(table);
                }

                foreach (ISoldProductModel item in sold)
                {
                    soldProductData.Delete(item.ID);
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
        // add security to menus
        // Change home controller views
        // Add Comments
        // check view bags
        // refactoring the code
        // edit price with coma
        // add remove range method to delete all products by table
        // add button order by...
        // add pages for errors
        // dont allow delete tables with products and so on
    }
}