using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework;
using CodeLibrary.EntityFramework.Models;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess
{
    public class ProductDataAccess : IDataAccessSubCategory<IProductModel>
    {
        private RestaurantContext _db;

        public ProductDataAccess(RestaurantContext db)
        {
            _db = db;
        }

        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (_db.Products.Where(x => x.Name == name).FirstOrDefault() != null)
            {
                exists = true;
            }

            return exists;
        }

        public void Create(IProductModel model)
        {
            Product item = new Product();

            item.Name = model.Name;
            item.CategoryID = model.CategoryID;
            item.Price = model.Price;
            _db.Products.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _db.Products.Find(id);
            _db.Products.Remove(item);
            _db.SaveChanges();
        }

        public IProductModel FindById(int id)
        {
            Product item = _db.Products.Find(id);
            IProductModel model = MapTheProductObject(item);

            return model;
        }

        public List<IProductModel> GetAll()
        {
            List<Product> list = _db.Products.ToList();
            List<IProductModel> modelList = Factory.InstanceIProductModelList();

            foreach (Product item in list)
            {
                modelList.Add(MapTheProductObject(item));
            }

            return modelList;
        }

        public List<IProductModel> GetBySubGroup(int id)
        {
            List<Product> list = _db.Products.Where(x => x.CategoryID == id).ToList();
            List<IProductModel> modelList = Factory.InstanceIProductModelList();

            foreach (Product item in list)
            {
                modelList.Add(MapTheProductObject(item));
            }

            return modelList;
        }

        public void Update(IProductModel model)
        {
            var item = _db.Products.Find(model.ID);

            item.Name = model.Name;
            item.Price = model.Price;
            item.CategoryID = model.CategoryID;

            _db.SaveChanges();
        }

        private IProductModel MapTheProductObject(Product item)
        {
            IProductModel model = Factory.InstanceProductModel();

            model.ID = item.ID;
            model.Name = item.Name;
            model.Price = item.Price;
            model.Category.ID = item.CategoryID;
            model.CategoryID = item.CategoryID;
            model.Category.Name = item.Category.Name;

            return model;
        }
    }
}