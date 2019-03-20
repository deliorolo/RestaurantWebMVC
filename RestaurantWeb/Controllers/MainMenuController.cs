using RestaurantWeb.Models;
using RestaurantWeb.Services;
using RestaurantWeb.Services.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWeb.Controllers
{
    public class MainMenuController : Controller
    {
        private IDataAccessSubCategory<IProductModel> productData = new ProductDataAccess();
        private IDataAccessRegular<ICategoryModel> categoryData = new CategoryDataAccess();
        private IDataAccessSubCategory<ITableModel> tableData = new TableDataAccess();
        private IDataAccessRegular<IAreaModel> areaData = new AreaDataAccess();
        private IDataAccessSubCategory<ISoldProductModel> soldProductData = new SoldProductDataAccess();
        private IDataAccessRegular<ISoldProductAccomplishedModel> soldProductAccomplishedData = new SoldProductAccomplishedDataAccess();

        private MainPageModel mainPageModel = new MainPageModel();

        public ActionResult Tables()
        {
            mainPageModel.Areas = areaData.GetAll();
            mainPageModel.Tables = tableData.GetAll();

            return View(mainPageModel);
        }

        public ActionResult TableCategories(int? id)
        {
            ITableModel table = new TableModel();
            mainPageModel.Tables = new List<ITableModel>();

            mainPageModel.Categories = categoryData.GetAll();
            table = tableData.FindById((int)id);
            mainPageModel.Tables.Add(table);

            return View(mainPageModel);
        }

        public ActionResult TableProducts(int? idTable, int? idCategory)
        {
            ITableModel table = new TableModel();
            mainPageModel.Tables = new List<ITableModel>();

            table = tableData.FindById((int)idTable);
            mainPageModel.Tables.Add(table);

            mainPageModel.Products = productData.GetBySubGroup((int)idCategory);

            return View(mainPageModel);
        }


        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("TableProducts")]
        public ActionResult TableAddProduct(int? idTable, int? idCategory, int? idProduct)
        {
            ITableModel table = new TableModel();
            IProductModel product = new ProductModel();
            ISoldProductModel soldProduct = new SoldProductModel();
            mainPageModel.Tables = new List<ITableModel>();

            table = tableData.FindById((int)idTable);
            product = productData.FindById((int)idProduct);

            soldProduct.Name = product.Name;
            soldProduct.Price = product.Price;
            soldProduct.CategoryID = product.CategoryID;
            soldProduct.TableID = (int)idTable;
            soldProduct.Detail = "";
            soldProduct.Category.ID = product.CategoryID;
            soldProduct.Category.Name = product.Category.Name;
            soldProductData.Create(soldProduct);

            table.Occupied = true;
            tableData.Update(table);
            table.SoldProducts.Add(soldProduct);

            mainPageModel.Products = productData.GetBySubGroup((int)idCategory);
            mainPageModel.Tables.Add(table);

            return View(mainPageModel);
        }

        public ActionResult PayAll(int? id)
        {
            ITableModel table = new TableModel();
            table = tableData.FindById((int)id);

            return View(table);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("PayAll")]
        public ActionResult PayAllConfirm(int? id)
        {
            List<ISoldProductModel> sold = soldProductData.GetBySubGroup((int)id);

            foreach (ISoldProductModel product in sold)
            {
                ISoldProductAccomplishedModel auxProduct = new SoldProductAccomplishedModel();
                auxProduct.Name = product.Name;
                auxProduct.CategoryID = product.CategoryID;
                auxProduct.Price = product.Price;
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
            ITableModel table = new TableModel();
            table = tableData.FindById((int)id);

            return View(table);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PayPartial(int? id, int[] Paid)
        {
            if (Paid != null)
            {
                List<ISoldProductModel> remaining = soldProductData.GetBySubGroup((int)id);
                List<ISoldProductModel> sold = new List<ISoldProductModel>();

                foreach (int item in Paid)
                {
                    ISoldProductAccomplishedModel auxProduct = new SoldProductAccomplishedModel();
                    ISoldProductModel product = new SoldProductModel();
                    product = remaining[item];

                    auxProduct.Name = product.Name;
                    auxProduct.CategoryID = product.CategoryID;
                    auxProduct.Price = product.Price;
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

            if (tableData.GetAll().Where(x => x.ID == product.TableID).FirstOrDefault().SoldProducts.Count() == 1)
            {
                tableData.GetAll().Where(x => x.ID == product.TableID).FirstOrDefault().Occupied = false;
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
        // avoid same names in registration
        // add remove range method to delete all products by table
        // add button order by...
        // add pages for errors
        // add number of products deleted when category
    }
}