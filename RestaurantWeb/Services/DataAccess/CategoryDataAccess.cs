using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class CategoryDataAccess : IDataAccessRegular<CategoryModel>
    {
        private RestaurantContext db = new RestaurantContext();

        public void Create(CategoryModel model)
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

        public CategoryModel Get(int id)
        {
            var item = db.Categories.Find(id);
            CategoryModel model = MapTheCategoryObject(item);

            return model;
        }

        public List<CategoryModel> GetAll()
        {
            var list = db.Categories.ToList();
            List<CategoryModel> modelList = new List<CategoryModel>();

            foreach (var item in list)
            {
                modelList.Add(MapTheCategoryObject(item));
            }

            return modelList;
        }

        public void Update(CategoryModel model)
        {
            var item = db.Categories.Find(model.ID);

            item.Name = model.Name;
            db.SaveChanges();
        }

        private CategoryModel MapTheCategoryObject(Category item)
        {
            CategoryModel model = new CategoryModel();

            model.ID = item.ID;
            model.Name = item.Name;

            return model;
        }
    }
}