using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services.DataAccess
{
    public class SoldProductAccomplishedDataAccess : IDataAccessRegular<SoldProductAccomplishedModel>
    {
        private RestaurantContext db = new RestaurantContext();

        public void Create(SoldProductAccomplishedModel model)
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

        public SoldProductAccomplishedModel Get(int id)
        {
            SoldProductAccomplished item = db.SoldProductsAccomplished.Find(id);
            SoldProductAccomplishedModel model = MapTheSoldProductAccomplishedObject(item);

            return model;
        }

        public List<SoldProductAccomplishedModel> GetAll()
        {
            List<SoldProductAccomplished> list = db.SoldProductsAccomplished.ToList();
            List<SoldProductAccomplishedModel> modelList = new List<SoldProductAccomplishedModel>();

            foreach (var item in list)
            {
                modelList.Add(MapTheSoldProductAccomplishedObject(item));
            }

            return modelList;
        }

        public void Update(SoldProductAccomplishedModel model)
        {

        }

        private SoldProductAccomplishedModel MapTheSoldProductAccomplishedObject(SoldProductAccomplished item)
        {
            SoldProductAccomplishedModel model = new SoldProductAccomplishedModel();

            model.ID = item.ID;
            model.Name = item.Name;
            model.CategoryID = item.CategoryID;
            model.Category.ID = item.CategoryID;           
            model.Price = item.Price;

            return model;
        }
    }
}