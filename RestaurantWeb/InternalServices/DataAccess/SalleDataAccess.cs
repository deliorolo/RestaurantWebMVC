using RestaurantWeb.AccessoryCode;
using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.InternalServices
{
    public class SalleDataAccess : ISalleDataAccess
    {
        private RestaurantContext db = Factory.InstanceRestaurantContext();

        public void EraseDataFromSalleList()
        {
            List<SoldProductAccomplished> products = db.SoldProductsAccomplished.ToList();
            db.SoldProductsAccomplished.RemoveRange(products);
            db.SaveChanges();
        }

        public List<ISalleModel> GetSalleList()
        {
            List<ISalleModel> salles = Factory.InstanceISalleModelList();
            List<SoldProductAccomplished> products = db.SoldProductsAccomplished.ToList();

            foreach (SoldProductAccomplished product in products)
            {
                ISalleModel salleAux = Factory.InstanceSalleModel();

                salleAux = salles.Where(x => x.Name == product.Name && x.CategoryName == product.Category.Name).FirstOrDefault();

                if (salleAux == null)
                {
                    ISalleModel salle = Factory.InstanceSalleModel();

                    salle.Name = product.Name;
                    salle.CategoryName = product.Category.Name;
                    salle.Price = product.Price;
                    salle.Ammount = 1;

                    salles.Add(salle);
                }
                else
                {
                    salles.Where(x => x.Name == product.Name && x.CategoryName == product.Category.Name).FirstOrDefault().Ammount++;
                    salles.Where(x => x.Name == product.Name && x.CategoryName == product.Category.Name).FirstOrDefault().Price += product.Price;
                }
            }

            return salles;
        }
    }
}