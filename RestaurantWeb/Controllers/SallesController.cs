using RestaurantWeb.Models;
using RestaurantWeb.Services;
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
        private ISalleDataAccess salleData = new SalleDataAccess();
        private IDataAccessSubCategory<ISoldProductModel> soldProductsData = new SoldProductDataAccess();

        public ActionResult Menu()
        {
            List<ISalleModel> salles = new List<ISalleModel>();

            salles = salleData.GetSalleList();

            return View(salles.OrderByDescending(x => x.Ammount));
        }

        [HttpPost, ActionName("Menu")]
        [ValidateAntiForgeryToken]
        public ActionResult ClosetheDay()
        {
            if (soldProductsData.GetAll().Count < 1)
            {
                List<ISalleModel> salles = new List<ISalleModel>();

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

        public ActionResult DownloadLastProductsSoldFile()
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");
            FileInfo file = directory.GetFiles().OrderByDescending(x => x.LastWriteTime).FirstOrDefault();

            if (file != null)
            {
                string filePath = file.FullName;
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(filePath, contentType, Path.GetFileName(filePath));
            }

            return View("FileNotFound");
        }

        public ActionResult DownloadProductsSoldFile(string fileName)
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");
            FileInfo file = directory.GetFiles().Where(x => x.Name == fileName).FirstOrDefault();

            if (file != null)
            {
                string filePath = file.FullName;
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(filePath, contentType, Path.GetFileName(filePath));
            }

            return View("FileNotFound");
        }

        public ActionResult ListAllLastProductsSoldFile()
        {
            List<string> fileNames = new List<string>();
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");
            DirectoryInfo directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");

            ICollection<FileInfo> files = directory.GetFiles();

            if(files.Count < 1)
            {
                return View("FileNotFound");
            }

            foreach (FileInfo file in files)
            {
                string name = "";
                name = file.Name;
                fileNames.Add(name);
            }

            return View(fileNames);
        }
    }
}