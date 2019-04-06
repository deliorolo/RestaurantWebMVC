using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeLibrary.AccessoryCode
{
    public static class ReadWriteFiles
    {
        /// <summary>
        /// It adds lines to an csv file with the time and money accomplished
        /// </summary>
        public static void WriteInDailyIncomeFile(string day, string hours, decimal money)
        {
            bool fileExist = false;
            StringBuilder sb = new StringBuilder();
            string separator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;

            fileExist = File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\DailyIncome.csv");

            if (fileExist)
            {
                string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\DailyIncome.csv", Encoding.UTF8);

                sb.Append(text);
            }
            else
            {
                sb.Append("Day");
                sb.Append(separator);
                sb.Append("Time");
                sb.Append(separator);
                sb.Append("Income");
                sb.AppendLine();
            }

            sb.Append(day);
            sb.Append(separator);
            sb.Append(hours);
            sb.Append(separator);
            sb.Append(money);
            sb.Append(' ');
            sb.Append('€');
            sb.AppendLine();

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\DailyIncome.csv", sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// It creates a new file with time and list of all sold products
        /// </summary>
        public static void AddFileOfTodaySoldProducts(List<ISalleModel> soldProducts, DateTime time)
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List");

            StringBuilder sb = new StringBuilder();
            decimal totalPriceAll = 0;

            string day = time.ToString("yyyy-MM-dd");
            string hour = time.ToString("HH-mm-ss");
            string hours = hour.Substring(0, 2);
            string minutes = hour.Substring(3, 2);
            string seconds = hour.Substring(6, 2);
            string fileName = day + " # " + hours + "h" + minutes + "m" + seconds + "s" + ".csv";

            sb.Append("Ammount");
            sb.Append(';');
            sb.Append("Name");
            sb.Append(';');
            sb.Append("Category");
            sb.Append(';');
            sb.Append("money");
            sb.AppendLine();
            sb.AppendLine();

            foreach (ISalleModel p in soldProducts)
            {
                if (p.Name != "***null***")
                {
                    int ammount = soldProducts.FindAll(x => x.Name == p.Name).Sum(x => x.Ammount);
                    string name = p.Name;
                    decimal totalPrice = 0;

                    totalPrice = soldProducts.FindAll(x => x.Name == p.Name).Sum(x => x.Price);
                    totalPriceAll += totalPrice;

                    sb.Append(ammount);
                    sb.Append(';');
                    sb.Append(p.Name);
                    sb.Append(';');
                    sb.Append(p.CategoryName);
                    sb.Append(';');
                    sb.Append(totalPrice);
                    sb.Append(" €");
                    sb.AppendLine();

                    while (soldProducts.Where(x => x.Name == name).FirstOrDefault() != null)
                    {
                        soldProducts.Where(x => x.Name == name).FirstOrDefault().Name = "***null***";
                    }
                }
            }

            sb.AppendLine();
            sb.Append("Total");
            sb.Append(";;;");
            sb.Append(totalPriceAll);
            sb.Append(" €");
            sb.AppendLine();

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Products Sold List\" + fileName, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// It checks if the DailyIncome.csv exists
        /// </summary>
        public static bool DailyIncomeFileExist()
        {
            bool fileExist = false;
            fileExist = File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\DailyIncome.csv");

            return fileExist;
        }
    }
}
