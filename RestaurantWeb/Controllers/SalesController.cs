using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;

namespace RestaurantWeb.Controllers
{
    public class SalesController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ISaleDataAccess saleData = Factory.InstanceSaleDataAccess();
        private ISoldProductDataAccess soldProductsData = Factory.InstanceSoldProductDataAccess();

        // Sales menu from all sold products from the day
        [Authorize]
        public ActionResult Menu(string order)
        {
            try
            {
                List<ISaleModel> sales = saleData.GetSaleList();

                // Selection of the order of the list
                switch (order)
                {
                    case "product":
                        return View(sales.OrderBy(x => x.Name));
                    case "category":
                        return View(sales.OrderBy(x => x.CategoryName));
                    case "sum":
                        return View(sales.OrderByDescending(x => x.Price));
                    default:
                        return View(sales.OrderByDescending(x => x.Ammount));
                }
            }
            catch (Exception ex)
            {
                log.Error("Could't load sales from Database", ex);
                return View("ErrorRetriveData");
            }        
        }

        // Closes the day and creates files in order to save the data
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Menu")]
        [ValidateAntiForgeryToken]
        public ActionResult ClosetheDay()
        {
            try
            {
                // It checks if all tables on the system are empty
                if (soldProductsData.GetAll().Count < 1)
                {
                    List<ISaleModel> sales = saleData.GetSaleList();

                    // Checks if there are sold products on this day
                    if (sales.Count < 1)
                    {
                        log.Info("User tried to close the day without sold products");
                        return View("NoSoldProducts");
                    }

                    DateTime time = DateTime.Now;
                    string day = time.ToString("dd-MM-yyyy");
                    string hours = time.ToString("HH:mm");

                    decimal money = sales.Sum(x => x.Price);
                  
                    try
                    {
                        // Creates and adds data to files about time, money income and list of sold products
                        ReadWriteFiles.AddFileOfTodaySoldProducts(sales, time);
                        ReadWriteFiles.WriteInDailyIncomeFile(day, hours, money);
                    }
                    catch (Exception exFile)
                    {
                        log.Error("Could't write to files", exFile);
                        return View("ErrorWriteToFile");
                    }

                    // Deletes all list of sold products on this day from database
                    saleData.EraseDataFromSaleList();

                    return RedirectToAction("Menu");
                }
                else
                {
                    log.Info("User tried to close the day with opened tables");
                    return View("OpenedTables");
                }
            }
            catch (Exception ex)
            {
                log.Error("Could't load data from Database", ex);
                return View("ErrorRetriveData");
            }
        }

        // It downloads the csv file where the system writes all money income from each day
        [Authorize(Roles = "admin")]
        public ActionResult DownloadMoneyIncomeFile()
        {
            if (ReadWriteFiles.DailyIncomeFileExist())
            {
                try
                {
                    string file = AppDomain.CurrentDomain.BaseDirectory + @"\DailyIncome.csv";
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(file, contentType, Path.GetFileName(file));
                }
                catch (Exception ex)
                {
                    log.Error("Could't download DailyIncome.csv", ex);
                    return View("ErrorDownload");
                }
            }

            log.Info("DailyIncome.csv doesn't exist");
            return View("FileNotFound");
        }

        // It downloads the csv file from the last day closed where is the info from all products sold
        [Authorize(Roles = "admin")]
        public ActionResult DownloadLastProductsSoldFile()
        {
           FileInfo file = FilesDownload.GetDirectoryProductsSoldList().GetFiles().OrderByDescending(x => x.LastWriteTime).FirstOrDefault();

            if (file != null)
            {
                try
                {
                    string filePath = file.FullName;
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(filePath, contentType, Path.GetFileName(filePath));
                }
                catch (Exception ex)
                {
                    log.Error("Could't download daily last products sold file", ex);
                    return View("ErrorDownload");
                }
            }

            log.Info("The daily last products sold file doesn't exist");
            return View("FileNotFound");
        }

        // It downloads the selected csv file where is the info from all products sold
        [Authorize(Roles = "admin")]
        public ActionResult DownloadProductsSoldFile(string fileName)
        {           
            FileInfo file = FilesDownload.GetDirectoryProductsSoldList().GetFiles().Where(x => x.Name == fileName).FirstOrDefault();

            if (file != null)
            {
                try
                {
                    string filePath = file.FullName;
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(filePath, contentType, Path.GetFileName(filePath));
                }
                catch (Exception ex)
                {
                    log.Error("Could't download daily products sold file", ex);
                    return View("ErrorDownload");
                }
            }

            log.Info("The daily products sold file doesn't exist");
            return View("FileNotFound");
        }

        // It gets the list of files with info of sold products from all days from its directory
        [Authorize(Roles = "admin")]
        public ActionResult ListAllLastProductsSoldFile()
        {
            try
            {
                ICollection<FileInfo> files = FilesDownload.GetDirectoryProductsSoldList().GetFiles();

                if (files.Count < 1)
                {
                    log.Info("User tried to access to an empty folder of daily products sold file");
                    return View("FileNotFound");
                }

                return View(FilesDownload.GetListFilesNames(files));
            }

            catch (Exception ex)
            {
                log.Error("Could't get list of daily products sold file", ex);
                return View("ErrorDownload");
            }           
        }
    }
}