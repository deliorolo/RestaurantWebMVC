using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class SoldProductDataAccess : IDataAccessSubCategory<ISoldProductModel>
    {
        private RestaurantContext db = new RestaurantContext();

        public bool CheckIfAlreadyExist(string name)
        {
            return false;
        }

        public void Create(ISoldProductModel model)
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

        public ISoldProductModel FindById(int id)
        {
            SoldProduct item = db.SoldProducts.Find(id);
            ISoldProductModel model = MapTheSoldProductObject(item);

            return model;
        }

        public List<ISoldProductModel> GetAll()
        {
            List<SoldProduct> list = db.SoldProducts.ToList();
            List<ISoldProductModel> modelList = new List<ISoldProductModel>();

            foreach (var item in list)
            {
                modelList.Add(MapTheSoldProductObject(item));
            }

            return modelList;
        }

        public List<ISoldProductModel> GetBySubGroup(int id)
        {
            List<SoldProduct> list = db.SoldProducts.Where(x => x.TableID == id).ToList();
            List<ISoldProductModel> modelList = new List<ISoldProductModel>();

            foreach (var item in list)
            {               
                modelList.Add(MapTheSoldProductObject(item));
            }

            return modelList;
        }

        public void Update(ISoldProductModel model)
        {
            var item = db.SoldProducts.Find(model.ID);

            item.Price = model.Price;
            item.Detail = model.Detail;

            db.SaveChanges();
        }

        private ISoldProductModel MapTheSoldProductObject(SoldProduct item)
        {
            ISoldProductModel model = new SoldProductModel();

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