using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services.DataAccess
{
    public class SoldProductAccomplishedDataAccess : IDataAccessRegular<ISoldProductAccomplishedModel>
    {
        private RestaurantContext db = new RestaurantContext();

        public void Create(ISoldProductAccomplishedModel model)
        {
            SoldProductAccomplished item = new SoldProductAccomplished();

            item.Name = model.Name;
            item.CategoryID = model.CategoryID;
            item.Price = model.Price;
            db.SoldProductsAccomplished.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.SoldProductsAccomplished.Find(id);
            db.SoldProductsAccomplished.Remove(item);
            db.SaveChanges();
        }

        public ISoldProductAccomplishedModel Get(int id)
        {
            SoldProductAccomplished item = db.SoldProductsAccomplished.Find(id);
            ISoldProductAccomplishedModel model = MapTheSoldProductAccomplishedObject(item);

            return model;
        }

        public List<ISoldProductAccomplishedModel> GetAll()
        {
            List<SoldProductAccomplished> list = db.SoldProductsAccomplished.ToList();
            List<ISoldProductAccomplishedModel> modelList = new List<ISoldProductAccomplishedModel>();

            foreach (var item in list)
            {
                modelList.Add(MapTheSoldProductAccomplishedObject(item));
            }

            return modelList;
        }

        public void Update(ISoldProductAccomplishedModel model)
        {

        }

        private ISoldProductAccomplishedModel MapTheSoldProductAccomplishedObject(SoldProductAccomplished item)
        {
            ISoldProductAccomplishedModel model = new SoldProductAccomplishedModel();

            model.ID = item.ID;
            model.Name = item.Name;
            model.CategoryID = item.CategoryID;
            model.Category.ID = item.CategoryID;           
            model.Price = item.Price;

            return model;
        }
    }
}