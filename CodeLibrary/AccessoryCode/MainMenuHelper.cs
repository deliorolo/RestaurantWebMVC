using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using System.Collections.Generic;
using System.Linq;

namespace CodeLibrary.AccessoryCode
{
    public static class MainMenuHelper
    {
        /// <summary>
        /// It completes the payment of selected items in a list based in indexes from variable "Paid"
        /// </summary>
        public static void PaySelectedSoldProducts(List<ISoldProductModel> fullList, int[] Paid, ISoldProductDataAccess soldProductData, ISoldProductAccomplishedDataAccess soldProductAccomplishedData)
        {
            List<ISoldProductModel> sold = Factory.InstanceISoldProductModelList();

            foreach (int item in Paid)
            {
                ISoldProductModel product = Factory.InstanceSoldProductModel();
                product = fullList[item];

                ISoldProductAccomplishedModel auxProduct = MappingObjects.SoldProductToSoldProductAccomplished(product);

                soldProductAccomplishedData.Create(auxProduct);
                sold.Add(product);
            }

            foreach (ISoldProductModel element in sold)
            {
                fullList.Remove(element);
            }

            soldProductData.DeleteList(sold);
        }

        /// <summary>
        /// It completes the payment for all products of the List
        /// </summary>
        public static void PaySoldProducts (List<ISoldProductModel> sold, ISoldProductDataAccess soldProductData, ISoldProductAccomplishedDataAccess soldProductAccomplishedData)
        {
            foreach (ISoldProductModel product in sold)
            {
                ISoldProductAccomplishedModel auxProduct = MappingObjects.SoldProductToSoldProductAccomplished(product);
                soldProductAccomplishedData.Create(auxProduct);
            }

            soldProductData.DeleteList(sold);
        }

        /// <summary>
        /// It orders the list of Sold Products by order selected
        /// </summary>
        public static List<ISoldProductModel> OrderListSoldProducts (List<ISoldProductModel> list, string order)
        {
            switch (order)
            {
                case "product":
                    list = list.OrderBy(x => x.Name).ToList();
                    break;
                case "category":
                    list = list.OrderBy(x => x.Category.Name).ToList();
                    break;
                case "detail":
                    list = list.OrderBy(x => x.Detail).ToList();
                    break;
                case "price":
                    list = list.OrderByDescending(x => x.Price).ToList();
                    break;
                default:
                    break;
            }

            return list;
        }
    }
}