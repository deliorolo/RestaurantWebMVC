using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework;
using CodeLibrary.EntityFramework.Models;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess
{
    public class SalleDataAccess : ISalleDataAccess
    {
        private RestaurantContext _db;

        public SalleDataAccess(RestaurantContext db)
        {
            _db = db;
        }

        public void EraseDataFromSalleList()
        {
            List<SoldProductAccomplished> products = _db.SoldProductsAccomplished.ToList();
            _db.SoldProductsAccomplished.RemoveRange(products);
            _db.SaveChanges();
        }

        public List<ISalleModel> GetSalleList()
        {
            List<ISalleModel> salles = Factory.InstanceISalleModelList();
            List<SoldProductAccomplished> products = _db.SoldProductsAccomplished.ToList();

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