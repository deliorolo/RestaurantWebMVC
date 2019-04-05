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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            catch (Exception ex)
            {
                log.Error("Could't load areas or tables from Database", ex);
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
                        log.Error("Could't find a table in the Database - return null");
                        return View("ErrorTable");
                    }

                    mainPageModel.Tables.Add(table);
                }
                catch (Exception ex)
                {
                    log.Error("Could't load categories or tables from Database", ex);
                    return View("ErrorRetriveData");
                }

                return View(mainPageModel); 
            }
            else
            {
                log.Error("The table ID was null while trying to access");
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
                catch (Exception ex)
                {
                    log.Error("Could't load products or tables from Database", ex);
                    return View("ErrorRetriveData");
                }

                return View(mainPageModel); 
            }
            else
            {
                log.Error("The category ID was null while trying to access");
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
                        log.Error("Could't find a product in the Database - return null");
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
                catch (Exception ex)
                {
                    log.Error("Could't load products, sold products or tables from Database", ex);
                    return View("ErrorRetriveData");
                }

                return View(mainPageModel); 
            }
            else
            {
                log.Error("The product ID was null while trying to access");
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
                catch (Exception ex)
                {
                    log.Error("Could't load tables from Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The table ID was null while trying to access");
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
            catch (Exception ex)
            {
                log.Error("Could't load sold products or tables from Database", ex);
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
                catch (Exception ex)
                {
                    log.Error("Could't load tables from Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The table ID was null while trying to access");
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
                catch (Exception ex)
                {
                    log.Error("Could't load sold products or tables from Database", ex);
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
                        log.Error("Could't find a product in the Database - return null");
                        return View("ErrorDeleteProduct");
                    }
                    return View(product);
                }

                catch (Exception ex)
                {
                    log.Error("Could't load product from Database", ex);
                    return View("ErrorRetriveData");
                }               
            }
            else
            {
                log.Error("The sold product ID was null while trying to access");
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

                return RedirectToAction("TableCategories", new { id = product.TableID });
            }
            catch (Exception ex)
            {
                log.Error("Could't load sold products or tables from Database", ex);
                return View("ErrorRetriveData");
            }
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
                        log.Error("Could't find a product in the Database - return null");
                        return View("ErrorEditProduct");
                    }
                    return View(product);
                }

                catch (Exception ex)
                {
                    log.Error("Could't load product from Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The sold product ID was null while trying to access");
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
                    return RedirectToAction("TableCategories", new { id = soldProductData.FindById(model.ID).TableID });
                }
                catch (Exception ex)
                {
                    log.Error("Could't load sold product from Database", ex);
                    return View("ErrorRetriveData");
                }
            }
            else
            {
                log.Error("The model state of the  sold product is invalid");
                return View("ErrorEditProduct");
            }
        }

        //TODO list
        // verify all buttons
        // Add Comments
    }
}