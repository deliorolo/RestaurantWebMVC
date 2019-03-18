using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class SalleDataAccess : ISalleDataAccess
    {
        private RestaurantContext db = new RestaurantContext();

        public void EraseDataFromSalleList()
        {
            List<SoldProductAccomplished> products = db.SoldProductsAccomplished.ToList();
            db.SoldProductsAccomplished.RemoveRange(products);
            db.SaveChanges();
        }

        public List<ISalleModel> GetSalleList()
        {
            List<ISalleModel> salles = new List<ISalleModel>();
            List<SoldProductAccomplished> products = db.SoldProductsAccomplished.ToList();

            foreach (SoldProductAccomplished product in products)
            {
                ISalleModel salleAux = new SalleModel();

                salleAux = salles.Where(x => x.Name == product.Name && x.CategoryName == product.Category.Name).FirstOrDefault();

                if (salleAux == null)
                {
                    ISalleModel salle = new SalleModel();

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