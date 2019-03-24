using RestaurantWeb.AccessoryCode;
using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.InternalServices
{
    public class CategoryDataAccess : IDataAccessRegular<ICategoryModel>
    {
        private RestaurantContext db = Factory.InstanceRestaurantContext();

        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (db.Categories.Where(x => x.Name == name).FirstOrDefault() != null)
            {
                exists = true;
            }

            return exists;
        }

        public void Create(ICategoryModel model)
        {
            Category item = new Category();

            item.Name = model.Name;
            db.Categories.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.Categories.Find(id);
            db.Categories.Remove(item);
            db.SaveChanges();
        }

        public ICategoryModel FindById(int id)
        {
            var item = db.Categories.Find(id);
            ICategoryModel model = MapTheCategoryObject(item);

            return model;
        }

        public List<ICategoryModel> GetAll()
        {
            var list = db.Categories.ToList();
            List<ICategoryModel> modelList = Factory.InstanceICategoryModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheCategoryObject(item));
            }

            return modelList;
        }

        public void Update(ICategoryModel model)
        {
            var item = db.Categories.Find(model.ID);

            item.Name = model.Name;
            db.SaveChanges();
        }

        private ICategoryModel MapTheCategoryObject(Category item)
        {
            ICategoryModel model = Factory.InstanceCategoryModel();

            model.ID = item.ID;
            model.Name = item.Name;

            return model;
        }
    }
}