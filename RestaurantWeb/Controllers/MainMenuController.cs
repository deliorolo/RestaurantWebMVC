using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;

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
            try
            {
                mainPageModel.Areas = areaData.GetAll();
                mainPageModel.Tables = tableData.GetAll();
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }

            return View(mainPageModel);
        }

        public ActionResult TableCategories(int? id)
        {
            if (id != null)
            {
                try
                {
                    mainPageModel.Categories = categoryData.GetAll();
                    ITableModel table = tableData.FindById((int)id);

                    if(table == null)
                    {
                        return View("ErrorTable");
                    }

                    mainPageModel.Tables.Add(table);
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }

                return View(mainPageModel); 
            }
            else
            {
                return View("ErrorTable");
            }
        }

        public ActionResult TableProducts(int? idTable, int? idCategory)
        {
            if (idCategory != null)
            {
                try
                {
                    ITableModel table = tableData.FindById((int)idTable);
                    mainPageModel.Tables.Add(table);

                    mainPageModel.Products = productData.GetBySubGroup((int)idCategory);
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }

                return View(mainPageModel); 
            }
            else
            {
                return View("ErrorCategory");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("TableProducts")]
        public ActionResult TableAddProduct(int? idTable, int? idCategory, int? idProduct)
        {
            if (idProduct != null)
            {
                try
                {
                    ITableModel table = tableData.FindById((int)idTable);
                    IProductModel product = productData.FindById((int)idProduct);

                    if (product == null)
                    {
                        return View("ErrorAddProduct");
                    }

                    ISoldProductModel soldProduct = MappingObjects.ProductToSoldProduct(product, (int)idTable, tableData);

                    soldProductData.Create(soldProduct);

                    table.Occupied = true;
                    tableData.Update(table);
                    table.SoldProducts = soldProductData.GetByTable(table.ID);

                    mainPageModel.Products = productData.GetBySubGroup((int)idCategory);

                    mainPageModel.Tables.Add(table);
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }

                return View(mainPageModel); 
            }
            else
            {
                return View("ErrorAddProduct");
            }
        }

        public ActionResult PayAll(int? id)
        {
            if (id != null)
            {
                try
                {
                    ITableModel table = tableData.FindById((int)id);
                    return View(table);
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                return View("ErrorTable");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("PayAll")]
        public ActionResult PayAllConfirm(int id)
        {
            try
            {
                List<ISoldProductModel> sold = soldProductData.GetByTable(id);

                MainMenuHelper.PaySoldProducts(sold, soldProductData, soldProductAccomplishedData);

                ITableModel table = tableData.FindById(id);
                table.Occupied = false;
                tableData.Update(table);
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Tables");
        }

        public ActionResult PayPartial(int? id)
        {
            if (id != null)
            {
                try
                {
                    ITableModel table = tableData.FindById((int)id);
                    return View(table);
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                return View("ErrorTable");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PayPartial(int id, int[] Paid)
        {
            if (Paid != null)
            {
                try
                {
                    List<ISoldProductModel> fullList = soldProductData.GetByTable(id);
                    MainMenuHelper.PaySelectedSoldProducts(fullList, Paid, soldProductData, soldProductAccomplishedData);

                    if (fullList.Count() < 1)
                    {
                        ITableModel table = tableData.FindById(id);
                        table.Occupied = false;
                        tableData.Update(table);
                    }
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }
            }

            return RedirectToAction("Tables");
        }

        public ActionResult Delete(int? idItem)
        {
            if (idItem != null)
            {
                try
                {
                    ISoldProductModel product = soldProductData.FindById((int)idItem);

                    if (product == null)
                    {
                        return View("ErrorDeleteProduct");
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
                return View("ErrorDeleteProduct");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idItem)
        {
            try
            {
                ISoldProductModel product = soldProductData.FindById(idItem);
                ITableModel table = tableData.GetAll().Where(x => x.ID == product.TableID).FirstOrDefault();

                if (table.SoldProducts.Count() == 1)
                {
                    table.Occupied = false;
                    tableData.Update(table);
                }

                soldProductData.Delete(idItem);
            }
            catch (Exception)
            {
                return View("ErrorRetriveData");
            }

            return RedirectToAction("Tables");
        }

        public ActionResult Edit(int? idItem)
        {
            if (idItem != null)
            {
                try
                {
                    ISoldProductModel product = soldProductData.FindById((int)idItem);

                    if (product == null)
                    {
                        return View("ErrorEditProduct");
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
                return View("ErrorEditProduct");
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed([Bind(Include = "ID, Price, Detail")] SoldProductModel product)
        {
            if (ModelState.IsValid)
            {
                ISoldProductModel model = product;

                try
                {
                    soldProductData.Update(model);
                }
                catch (Exception)
                {
                    return View("ErrorRetriveData");
                }

                return RedirectToAction("Tables");
            }
            else
            {
                return View("ErrorEditProduct");
            }
        }

        //TODO list
        // verify all buttons
        // Add Comments
    }
}