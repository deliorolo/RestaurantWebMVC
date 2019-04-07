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
        private readonly ISoldProductAccomplishedDataAccess soldProductAccomplishedData = Factory.InstanceSoldProductAccomplishedDataAccess();

        private IMainPageModel mainPageModel = Factory.InstanceMainPageModel();

        // Menu where is the list of all areas with its corresponding tables
        public ActionResult Tables()
        {
            try
            {
                // Joining list of areas and tables to a single model
                mainPageModel.Areas = areaData.GetAll().OrderBy(x => x.Name).ToList();
                mainPageModel.Tables = tableData.GetAll().OrderBy(x => x.NumberOfTable).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Could't load areas or tables from Database", ex);
                return View("ErrorRetriveData");
            }

            return View(mainPageModel);
        }

        // Menu where is a table with its products and available categories of products
        public ActionResult TableCategories(int? id, string order)
        {
            if (id != null)
            {
                try
                {
                    // Joining list of categories and selected table to a single model
                    mainPageModel.Categories = categoryData.GetAll().OrderBy(x => x.Name).ToList();
                    ITableModel table = tableData.FindById((int)id);

                    if(table == null)
                    {
                        log.Error("Could't find a table in the Database - return null");
                        return View("ErrorTable");
                    }

                    // Selection of the order of the list
                    table.SoldProducts = MainMenuHelper.OrderListSoldProducts(table.SoldProducts, order);

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

        // Menu where is a table with its products and available products do add a from selected category
        public ActionResult TableProducts(int? idTable, int? idCategory)
        {
            if (idCategory != null)
            {
                try
                {
                    // Joining list of products and selected table to a single model
                    ITableModel table = tableData.FindById((int)idTable);
                    mainPageModel.Tables.Add(table);

                    mainPageModel.Products = productData.GetBySubGroup((int)idCategory).OrderBy(x => x.Name).ToList();
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

        // Add a product to the selected table
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("TableProducts")]
        public ActionResult TableAddProduct(int? idTable, int? idCategory, int? idProduct)
        {
            if (idProduct != null)
            {
                try
                {
                    // Finding the selected product and the table where is going to be added
                    ITableModel table = tableData.FindById((int)idTable);
                    IProductModel product = productData.FindById((int)idProduct);

                    if (product == null)
                    {
                        log.Error("Could't find a product in the Database - return null");
                        return View("ErrorAddProduct");
                    }

                    // Creates a SoldProductModel from a ProductModel that can be added to a list in each TableModel
                    ISoldProductModel soldProduct = MappingObjects.ProductToSoldProduct(product, (int)idTable, tableData);
                    soldProductData.Create(soldProduct);

                    // Sets the current table as opened (Occupied by products)
                    table.Occupied = true;
                    tableData.Update(table);
                    table.SoldProducts = soldProductData.GetByTable(table.ID);

                    // Joins list of products and selected table to a single model
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

        // It shows a menu where is the list of all products on a table ready for payment
        public ActionResult PayAll(int? id, string order)
        {
            if (id != null)
            {
                try
                {
                    // Finding the selected table is order to list the Sold Products on it
                    ITableModel table = tableData.FindById((int)id);

                    // Selection of the order of the list
                    table.SoldProducts = MainMenuHelper.OrderListSoldProducts(table.SoldProducts, order);

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

        // Confirmation of all the products on the table to be paid
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("PayAll")]
        public ActionResult PayAllConfirm(int id)
        {
            try
            {
                // Finding the sold products from the table that are confirmed as paid
                List<ISoldProductModel> sold = soldProductData.GetByTable(id);

                // Moving in the database the sold products to a table of all sold products accomplished
                MainMenuHelper.PaySoldProducts(sold, soldProductData, soldProductAccomplishedData);

                //Set the table to empty
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

        // It shows a menu where is the list of all products on a table that can be selected for payment
        public ActionResult PayPartial(int? id, string order)
        {
            if (id != null)
            {
                try
                {
                    // Finding the selected table is order to list the Sold Products on it
                    ITableModel table = tableData.FindById((int)id);

                    // Selection of the order of the list
                    table.SoldProducts = MainMenuHelper.OrderListSoldProducts(table.SoldProducts, order);

                    table.OrderSoldProducts = order;
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

        // Confirmation of selected products on the table to be paid
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PayPartial(int id, int[] Paid, string order)
        {
            if (Paid != null)
            {
                try
                {
                    // Finding the sold products from the table that are selected to be paid
                    List<ISoldProductModel> fullList = soldProductData.GetByTable(id);

                    // Selection of the order of the list
                    fullList = MainMenuHelper.OrderListSoldProducts(fullList, order);

                    // Moving in the database the selected sold products to a table of all sold products accomplished
                    MainMenuHelper.PaySelectedSoldProducts(fullList, Paid, soldProductData, soldProductAccomplishedData);

                    //if the table is without any product set it to empty
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

        // It asks for a confirmation in deleting a selected product from the table
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

        // Confirmation of selected product on the table to be deleted
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idItem)
        {
            try
            {
                ISoldProductModel product = soldProductData.FindById(idItem);
                ITableModel table = tableData.GetAll().Where(x => x.ID == product.TableID).FirstOrDefault();

                //if the table is without any product set it to empty
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

        // It display a form in order to edit a selected product from the table
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

        // Confirmation of selected product on the table to be edited
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
    }
}