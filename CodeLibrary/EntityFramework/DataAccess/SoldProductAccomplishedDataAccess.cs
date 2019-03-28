//using RestaurantWeb.Models.EF;
using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework.Models;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.EntityFramework.DataAccess
{
    public class SoldProductAccomplishedDataAccess : ISoldProductAccomplishedDataAccess
    {
        private RestaurantContext _db;

        public SoldProductAccomplishedDataAccess(RestaurantContext db)
        {
            _db = db;
        }

        public void Create(ISoldProductAccomplishedModel model)
        {
            SoldProductAccomplished item = new SoldProductAccomplished();

            item.Name = model.Name;
            item.CategoryID = model.CategoryID;
            item.Price = model.Price;
            _db.SoldProductsAccomplished.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _db.SoldProductsAccomplished.Find(id);
            _db.SoldProductsAccomplished.Remove(item);
            _db.SaveChanges();
        }

        public ISoldProductAccomplishedModel FindById(int id)
        {
            SoldProductAccomplished item = _db.SoldProductsAccomplished.Find(id);
            ISoldProductAccomplishedModel model = MapTheSoldProductAccomplishedObject(item);

            return model;
        }

        public List<ISoldProductAccomplishedModel> GetAll()
        {
            List<SoldProductAccomplished> list = _db.SoldProductsAccomplished.ToList();
            List<ISoldProductAccomplishedModel> modelList = Factory.InstanceISoldProductAccomplishedModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheSoldProductAccomplishedObject(item));
            }

            return modelList;
        }

        public List<ISoldProductAccomplishedModel> GetByCategory(int id)
        {
            List<SoldProductAccomplished> list = _db.SoldProductsAccomplished.Where(x => x.CategoryID == id).ToList();
            List<ISoldProductAccomplishedModel> modelList = Factory.InstanceISoldProductAccomplishedModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheSoldProductAccomplishedObject(item));
            }

            return modelList;
        }

        private ISoldProductAccomplishedModel MapTheSoldProductAccomplishedObject(SoldProductAccomplished item)
        {
            ISoldProductAccomplishedModel model = Factory.InstanceSoldProductAccomplishedModel();

            model.ID = item.ID;
            model.Name = item.Name;
            model.CategoryID = item.CategoryID;
            model.Category.ID = item.CategoryID;           
            model.Price = item.Price;

            return model;
        }
    }
}