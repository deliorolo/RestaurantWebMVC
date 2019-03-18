using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class ProductDataAccess : IDataAccessSubCategory<ProductModel>
    {
        private RestaurantContext db = new RestaurantContext();

        public void Create(ProductModel model)
        {
            Product item = new Product();

            item.Name = model.Name;
            item.CategoryID = model.CategoryID;
            item.Price = model.Price;
            db.Products.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.Products.Find(id);
            db.Products.Remove(item);
            db.SaveChanges();
        }

        public ProductModel Get(int id)
        {
            Product item = db.Products.Find(id);
            ProductModel model = MapTheProductObject(item);

            return model;
        }

        public List<ProductModel> GetAll()
        {
            List<Product> list = db.Products.ToList();
            List<ProductModel> modelList = new List<ProductModel>();

            foreach (Product item in list)
            {
                modelList.Add(MapTheProductObject(item));
            }

            return modelList;
        }

        public List<ProductModel> GetByParameter(int id)
        {
            List<Product> list = db.Products.Where(x => x.CategoryID == id).ToList();
            List<ProductModel> modelList = new List<ProductModel>();

            foreach (Product item in list)
            {
                modelList.Add(MapTheProductObject(item));
            }

            return modelList;
        }

        public void Update(ProductModel model)
        {
            var item = db.Products.Find(model.ID);

            item.Name = model.Name;
            item.Price = model.Price;
            item.CategoryID = model.CategoryID;

            db.SaveChanges();
        }

        private ProductModel MapTheProductObject(Product item)
        {
            ProductModel model = new ProductModel();

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