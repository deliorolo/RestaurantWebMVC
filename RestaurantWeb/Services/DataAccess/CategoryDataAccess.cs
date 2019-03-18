using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class CategoryDataAccess : IDataAccessRegular<ICategoryModel>
    {
        private RestaurantContext db = new RestaurantContext();

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

        public ICategoryModel Get(int id)
        {
            var item = db.Categories.Find(id);
            ICategoryModel model = MapTheCategoryObject(item);

            return model;
        }

        public List<ICategoryModel> GetAll()
        {
            var list = db.Categories.ToList();
            List<ICategoryModel> modelList = new List<ICategoryModel>();

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
            ICategoryModel model = new CategoryModel();

            model.ID = item.ID;
            model.Name = item.Name;

            return model;
        }
    }
}