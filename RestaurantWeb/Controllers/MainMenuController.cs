using RestaurantWeb.Models;
using RestaurantWeb.Services;
using RestaurantWeb.Services.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWeb.Controllers
{
    public class MainMenuController : Controller
    {
        private IDataAccessSubCategory<ProductModel> productData = new ProductDataAccess();
        private IDataAccessRegular<CategoryModel> categoryData = new CategoryDataAccess();
        private IDataAccessSubCategory<TableModel> tableData = new TableDataAccess();
        private IDataAccessRegular<AreaModel> areaData = new AreaDataAccess();
        private IDataAccessSubCategory<SoldProductModel> soldProductData = new SoldProductDataAccess();
        private IDataAccessRegular<SoldProductAccomplishedModel> soldProductAccomplishedData = new SoldProductAccomplishedDataAccess();

        private MainPageModel mainPageModel = new MainPageModel();

        // GET: MainMenu
        public ActionResult Tables()
        {
            mainPageModel.Areas = areaData.GetAll();
            mainPageModel.Tables = tableData.GetAll();

            return View(mainPageModel);
        }

        public ActionResult TableCategories(int? id)
        {
            TableModel table = new TableModel();
            mainPageModel.Tables = new List<TableModel>();

            mainPageModel.Categories = categoryData.GetAll();
            table = tableData.Get((int)id);
            mainPageModel.Tables.Add(table);

            return View(mainPageModel);
        }

        public ActionResult TableProducts(int? idTable, int? idCategory)
        {
            TableModel table = new TableModel();
            mainPageModel.Tables = new List<TableModel>();

            table = tableData.Get((int)idTable);
            mainPageModel.Tables.Add(table);

            mainPageModel.Products = productData.GetByParameter((int)idCategory);

            return View(mainPageModel);
        }


        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("TableProducts")]
        public ActionResult TableAddProduct(int? idTable, int? idCategory, int? idProduct)
        {
            TableModel table = new TableModel();
            ProductModel product = new ProductModel();
            SoldProductModel soldProduct = new SoldProductModel();
            mainPageModel.Tables = new List<TableModel>();

            table = tableData.Get((int)idTable);
            product = productData.Get((int)idProduct);

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

            mainPageModel.Products = productData.GetByParameter((int)idCategory);
            mainPageModel.Tables.Add(table);

            return View(mainPageModel);
        }

        public ActionResult PayAll(int? id)
        {
            TableModel table = new TableModel();
            table = tableData.Get((int)id);

            return View(table);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("PayAll")]
        public ActionResult PayAllConfirm(int? id)
        {
            List<SoldProductModel> sold = soldProductData.GetByParameter((int)id);

            foreach (SoldProductModel product in sold)
            {
                SoldProductAccomplishedModel auxProduct = new SoldProductAccomplishedModel();
                auxProduct.Name = product.Name;
                auxProduct.CategoryID = product.CategoryID;
                auxProduct.Price = product.Price;
                soldProductAccomplishedData.Create(auxProduct);
            }

            foreach (SoldProductModel item in sold)
            {
                soldProductData.Delete(item.ID);
            }

            TableModel table = tableData.Get((int)id);
            table.Occupied = false;
            tableData.Update(table);

            return RedirectToAction("Tables");
        }

        public ActionResult PayPartial(int? id)
        {
            TableModel table = new TableModel();
            table = tableData.Get((int)id);

            return View(table);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PayPartial(int? id, int[] Paid)
        {
            if (Paid != null)
            {
                List<SoldProductModel> remaining = soldProductData.GetByParameter((int)id);
                List<SoldProductModel> sold = new List<SoldProductModel>();

                foreach (int item in Paid)
                {
                    SoldProductAccomplishedModel auxProduct = new SoldProductAccomplishedModel();
                    SoldProductModel product = new SoldProductModel();
                    product = remaining[item];

                    auxProduct.Name = product.Name;
                    auxProduct.CategoryID = product.CategoryID;
                    auxProduct.Price = product.Price;
                    soldProductAccomplishedData.Create(auxProduct);
                    sold.Add(product);
                }

                foreach (SoldProductModel element in sold)
                {
                    remaining.Remove(element);
                }

                if (remaining.Count() < 1)
                {
                    TableModel table = tableData.Get((int)id);
                    table.Occupied = false;
                    tableData.Update(table);
                }

                foreach (SoldProductModel item in sold)
                {
                    soldProductData.Delete(item.ID);
                }

            }

            return RedirectToAction("Tables");
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? idItem)
        {
            if (idItem == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SoldProductModel product = soldProductData.Get((int)idItem);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idItem)
        {
            SoldProductModel product = soldProductData.Get(idItem);

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
            SoldProductModel product = soldProductData.Get((int)idItem);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed([Bind(Include = "ID, Price, Detail")] SoldProductModel product)
        {
            if (ModelState.IsValid)
            {
                soldProductData.Update(product);
                return RedirectToAction("Tables");
            }

            return View(product);
        }

        //TODO list
        // verify all buttons
        // use sqlite
        // add [arguments to models]
        // add security to menus
        // Change home controller views
        // Add Comments
        // Add Interfaces
        // check view bags
        // refactoring the code
        // edit price with coma
        // avoid same names in registration
        // add remove range method to delete all products by table
    }
}