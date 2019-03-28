//using RestaurantWeb.Models.EF;
using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework.Models;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.EntityFramework.DataAccess
{
    public class CategoryDataAccess : IDataAccessRegular<ICategoryModel>
    {
        private RestaurantContext _db;

        public CategoryDataAccess(RestaurantContext db)
        {
            _db = db;
        }

        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (_db.Categories.Where(x => x.Name == name).FirstOrDefault() != null)
            {
                exists = true;
            }

            return exists;
        }

        public void Create(ICategoryModel model)
        {
            Category item = new Category();

            item.Name = model.Name;
            _db.Categories.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _db.Categories.Find(id);
            _db.Categories.Remove(item);
            _db.SaveChanges();
        }

        public ICategoryModel FindById(int id)
        {
            var item = _db.Categories.Find(id);
            ICategoryModel model = MapTheCategoryObject(item);

            return model;
        }

        public List<ICategoryModel> GetAll()
        {
            var list = _db.Categories.ToList();
            List<ICategoryModel> modelList = Factory.InstanceICategoryModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheCategoryObject(item));
            }

            return modelList;
        }

        public void Update(ICategoryModel model)
        {
            var item = _db.Categories.Find(model.ID);

            item.Name = model.Name;
            _db.SaveChanges();
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