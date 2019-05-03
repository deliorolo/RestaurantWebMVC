using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework;
using CodeLibrary.EntityFramework.Models;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess
{
    public class SaleDataAccess : ISaleDataAccess
    {
        private RestaurantContext _db;

        public SaleDataAccess(RestaurantContext db)
        {
            _db = db;
        }

        public void EraseDataFromSaleList()
        {
            List<SoldProductAccomplished> products = _db.SoldProductsAccomplished.ToList();
            _db.SoldProductsAccomplished.RemoveRange(products);
            _db.SaveChanges();
        }

        public List<ISaleModel> GetSaleList()
        {
            List<ISaleModel> sales = Factory.InstanceISaleModelList();
            List<SoldProductAccomplished> products = _db.SoldProductsAccomplished.ToList();

            foreach (SoldProductAccomplished product in products)
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