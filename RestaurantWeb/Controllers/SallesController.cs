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
    public class SallesController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ISalleDataAccess salleData = Factory.InstanceSalleDataAccess();
        private ISoldProductDataAccess soldProductsData = Factory.InstanceSoldProductDataAccess();

        [Authorize]
        public ActionResult Menu(string order)
        {
            try
            {
                List<ISalleModel> salles = salleData.GetSalleList();

                switch (order)
                {
                    case "product":
                        return View(salles.OrderBy(x => x.Name));
                    case "category":
                        return View(salles.OrderBy(x => x.CategoryName));
                    case "sum":
                        return View(salles.OrderByDescending(x => x.Price));
                    default:
                        return View(salles.OrderByDescending(x => x.Ammount));
                }
            }
            catch (Exception ex)
            {
                log.Error("Could't load salles from Database", ex);
                return View("ErrorRetriveData");
            }        
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Menu")]
        [ValidateAntiForgeryToken]
        public ActionResult ClosetheDay()
        {
            try
            {
                if (soldProductsData.GetAll().Count < 1)
                {
                    List<ISalleModel> salles = salleData.GetSalleList();

                    if (salles.Count < 1)
                    {
                        log.Info("User tried to close the day without sold products");
                        return View("NoSoldProducts");
                    }

                    DateTime time = DateTime.Now;
                    string day = time.ToString("dd-MM-yyyy");
                    string hours = time.ToString("HH:mm");

                    decimal money = salles.Sum(x => x.Price);

                    try
                    {
                        ReadWriteFiles.AddFileOfTodaySoldProducts(salles, time);
                        ReadWriteFiles.WriteInDailyIncomeFile(day, hours, money);
                    }
                    catch (Exception exFile)
                    {
                        log.Error("Could't write to files", exFile);
                        return View("ErrorWriteToFile");
                    }

                    salleData.EraseDataFromSalleList();

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