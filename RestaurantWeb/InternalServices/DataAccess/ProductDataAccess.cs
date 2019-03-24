﻿using RestaurantWeb.AccessoryCode;
using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.InternalServices
{
    public class ProductDataAccess : IDataAccessSubCategory<IProductModel>
    {
        private RestaurantContext db = Factory.InstanceRestaurantContext();

        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (db.Products.Where(x => x.Name == name).FirstOrDefault() != null)
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
            db.Products.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.Products.Find(id);
            db.Products.Remove(item);
            db.SaveChanges();
        }

        public IProductModel FindById(int id)
        {
            Product item = db.Products.Find(id);
            IProductModel model = MapTheProductObject(item);

            return model;
        }

        public List<IProductModel> GetAll()
        {
            List<Product> list = db.Products.ToList();
            List<IProductModel> modelList = Factory.InstanceIProductModelList();

            foreach (Product item in list)
            {
                modelList.Add(MapTheProductObject(item));
            }

            return modelList;
        }

        public List<IProductModel> GetBySubGroup(int id)
        {
            List<Product> list = db.Products.Where(x => x.CategoryID == id).ToList();
            List<IProductModel> modelList = Factory.InstanceIProductModelList();

            foreach (Product item in list)
            {
                modelList.Add(MapTheProductObject(item));
            }

            return modelList;
        }

        public void Update(IProductModel model)
        {
            var item = db.Products.Find(model.ID);

            item.Name = model.Name;
            item.Price = model.Price;
            item.CategoryID = model.CategoryID;

            db.SaveChanges();
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