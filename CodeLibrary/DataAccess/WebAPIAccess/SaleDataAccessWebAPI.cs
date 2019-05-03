using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class SaleDataAccessWebAPI : ISaleDataAccess
    {
        public void EraseDataFromSaleList()
        {
            ISoldProductAccomplishedDataAccess soldProductsAccomplishedData = Factory.InstanceSoldProductAccomplishedDataAccess();
            List<ISoldProductAccomplishedModel> products = soldProductsAccomplishedData.GetAll();

            foreach (var product in products)
            {
                soldProductsAccomplishedData.Delete(product.ID);
            }
        }

        public List<ISaleModel> GetSaleList()
        {
            List<ISaleModel> sales = Factory.InstanceISaleModelList();
            ISoldProductAccomplishedDataAccess soldProductsAccomplishedData = Factory.InstanceSoldProductAccomplishedDataAccess();
            List<ISoldProductAccomplishedModel> products = soldProductsAccomplishedData.GetAll();

            foreach (ISoldProductAccomplishedModel product in products)
            {
                ISaleModel saleAux = Factory.InstanceSaleModel();

                saleAux = sales.Where(x => x.Name == product.Name && x.CategoryName == product.Category.Name).FirstOrDefault();

                if (saleAux == null)
                {
                    ISaleModel sale = Factory.InstanceSaleModel();

                    sale.Name = product.Name;
                    sale.CategoryName = product.Category.Name;
                    sale.Price = product.Price;
                    sale.Ammount = 1;

                    sales.Add(sale);
                }
                else
                {
                    sales.Where(x => x.Name == product.Name && x.CategoryName == product.Category.Name).FirstOrDefault().Ammount++;
                    sales.Where(x => x.Name == product.Name && x.CategoryName == product.Category.Name).FirstOrDefault().Price += product.Price;
                }
            }

            return sales;
        }
    }
}
