using RestaurantWeb.InternalServices;
using RestaurantWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.AccessoryCode
{
    public static class MainMenuHelper
    {
        public static void PaySelectedSoldProducts(List<ISoldProductModel> fullList, int[] Paid, ISoldProductDataAccess soldProductData, ISoldProductAccomplishedDataAccess soldProductAccomplishedData)
        {
            List<ISoldProductModel> sold = ObjectCreator.ISoldProductModelList();

            foreach (int item in Paid)
            {
                ISoldProductModel product = ObjectCreator.SoldProductModel();
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

        public static void PaySoldProducts (List<ISoldProductModel> sold, ISoldProductDataAccess soldProductData, ISoldProductAccomplishedDataAccess soldProductAccomplishedData)
        {
            foreach (ISoldProductModel product in sold)
            {
                ISoldProductAccomplishedModel auxProduct = MappingObjects.SoldProductToSoldProductAccomplished(product);
                soldProductAccomplishedData.Create(auxProduct);
            }

            soldProductData.DeleteList(sold);
        }
    }
}