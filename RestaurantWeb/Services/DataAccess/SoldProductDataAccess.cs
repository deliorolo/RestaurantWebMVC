using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class SoldProductDataAccess : IDataAccessSubCategory<SoldProductModel>
    {
        private RestaurantContext db = new RestaurantContext();

        public void Create(SoldProductModel model)
        {
            SoldProduct item = new SoldProduct();

            item.Name = model.Name;
            item.CategoryID = model.CategoryID;
            item.TableID = model.TableID;
            item.Price = model.Price;
            item.Detail = model.Detail;
            db.SoldProducts.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.SoldProducts.Find(id);
            db.SoldProducts.Remove(item);
            db.SaveChanges();
        }

        public SoldProductModel Get(int id)
        {
            SoldProduct item = db.SoldProducts.Find(id);
            SoldProductModel model = MapTheSoldProductObject(item);

            return model;
        }

        public List<SoldProductModel> GetAll()
        {
            List<SoldProduct> list = db.SoldProducts.ToList();
            List<SoldProductModel> modelList = new List<SoldProductModel>();

            foreach (var item in list)
            {
                modelList.Add(MapTheSoldProductObject(item));
            }

            return modelList;
        }

        public List<SoldProductModel> GetByParameter(int id)
        {
            List<SoldProduct> list = db.SoldProducts.Where(x => x.TableID == id).ToList();
            List<SoldProductModel> modelList = new List<SoldProductModel>();

            foreach (var item in list)
            {               
                modelList.Add(MapTheSoldProductObject(item));
            }

            return modelList;
        }

        public void Update(SoldProductModel model)
        {
            var item = db.SoldProducts.Find(model.ID);

            item.Price = model.Price;
            item.Detail = model.Detail;

            db.SaveChanges();
        }

        private SoldProductModel MapTheSoldProductObject(SoldProduct item)
        {
            SoldProductModel model = new SoldProductModel();

            model.ID = item.ID;
            model.Name = item.Name;
            model.CategoryID = item.CategoryID;
            model.Category.ID = item.CategoryID;
            model.Category.Name = item.Category.Name;
            model.TableID = item.TableID;
            model.Table.ID = item.TableID;
            model.Price = item.Price;
            model.Detail = item.Detail;

            return model;
        }
    }
}