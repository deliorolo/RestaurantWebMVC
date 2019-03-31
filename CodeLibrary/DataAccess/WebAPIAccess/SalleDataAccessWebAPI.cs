using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLibrary.AccessoryCode;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class SalleDataAccessWebAPI : ISalleDataAccess
    {
        public void EraseDataFromSalleList()
        {
            ISoldProductAccomplishedDataAccess soldProductsAccomplishedData = Factory.InstanceSoldProductAccomplishedDataAccess();
            List<ISoldProductAccomplishedModel> products = soldProductsAccomplishedData.GetAll();

            foreach (var product in products)
            {
                soldProductsAccomplishedData.Delete(product.ID);
            }
        }

        public List<ISalleModel> GetSalleList()
        {
            List<ISalleModel> salles = Factory.InstanceISalleModelList();
            ISoldProductAccomplishedDataAccess soldProductsAccomplishedData = Factory.InstanceSoldProductAccomplishedDataAccess();
            List<ISoldProductAccomplishedModel> products = soldProductsAccomplishedData.GetAll();

            foreach (ISoldProductAccomplishedModel product in products)
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
