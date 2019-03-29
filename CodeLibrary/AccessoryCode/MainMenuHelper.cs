using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using System.Collections.Generic;

namespace CodeLibrary.AccessoryCode
{
    public static class MainMenuHelper
    {
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