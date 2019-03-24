using RestaurantWeb.AccessoryCode;
using RestaurantWeb.Models;
using RestaurantWeb.InternalServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantWeb.Controllers
{
    public class SallesController : Controller
    {
        private ISalleDataAccess salleData = Factory.InstanceSalleDataAccess();
        private ISoldProductDataAccess soldProductsData = Factory.InstanceSoldProductDataAccess();

        [Authorize]
        public ActionResult Menu(string order)
        {
            List<ISalleModel> salles = Factory.InstanceISalleModelList();
            salles = salleData.GetSalleList();
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

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Menu")]
        [ValidateAntiForgeryToken]
        public ActionResult ClosetheDay()
        {
            if (soldProductsData.GetAll().Count < 1)
            {
                List<ISalleModel> salles = Factory.InstanceISalleModelList();
                salles = salleData.GetSalleList();

                DateTime time = DateTime.Now;
                string day = time.ToString("dd-MM-yyyy");
                string hours = time.ToString("HH:mm");

                decimal money = salles.Sum(x => x.Price);

                ReadWriteFiles.AddFileOfTodaySoldProducts(salles, time);
                ReadWriteFiles.WriteInDailyIncomeFile(day, hours, money);

                salleData.EraseDataFromSalleList();

                return RedirectToAction("Menu"); 
            }
            else
            {
                return View("OpenedTables");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult DownloadMoneyIncomeFile()
        {
            if (ReadWriteFiles.DailyIncomeFileExist())
            {
                string file = AppDomain.CurrentDomain.BaseDirectory + @"\DailyIncome.csv";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(file, contentType, Path.GetFileName(file));
            }

            return View("FileNotFound");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DownloadLastProductsSoldFile()
        {
           FileInfo file = FilesDownload.GetDirectoryProductsSoldList().GetFiles().OrderByDescending(x => x.LastWriteTime).FirstOrDefault();

            if (file != null)
            {
                string filePath = file.FullName;
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(filePath, contentType, Path.GetFileName(filePath));
            }

            return View("FileNotFound");
        }

        [Authorize(Roles = "admin")]
        public ActionResult DownloadProductsSoldFile(string fileName)
        {           
            FileInfo file = FilesDownload.GetDirectoryProductsSoldList().GetFiles().Where(x => x.Name == fileName).FirstOrDefault();

            if (file != null)
            {
                string filePath = file.FullName;
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(filePath, contentType, Path.GetFileName(filePath));
            }

            return View("FileNotFound");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ListAllLastProductsSoldFile()
        {
            ICollection<FileInfo> files = FilesDownload.GetDirectoryProductsSoldList().GetFiles();

            if(files.Count < 1)
            {
                return View("FileNotFound");
            }

            return View(FilesDownload.GetListFilesNames(files));
        }
    }
}