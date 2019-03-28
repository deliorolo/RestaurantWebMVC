//using RestaurantWeb.Models.EF;
using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework.Models;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.EntityFramework.DataAccess
{
    public class SoldProductDataAccess : ISoldProductDataAccess
    {
        private RestaurantContext _db;

        public SoldProductDataAccess(RestaurantContext db)
        {
            _db = db;
        }

        public void Create(ISoldProductModel model)
        {
            SoldProduct item = new SoldProduct();

            item.Name = model.Name;
            item.CategoryID = model.CategoryID;
            item.TableID = model.TableID;
            item.Price = model.Price;
            item.Detail = model.Detail;

            _db.SoldProducts.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _db.SoldProducts.Find(id);
            _db.SoldProducts.Remove(item);
            _db.SaveChanges();
        }

        public void DeleteList(List<ISoldProductModel> list)
        {
            foreach (ISoldProductModel item in list)
            {
                Delete(item.ID);
            }
        }

        public ISoldProductModel FindById(int id)
        {
            SoldProduct item = _db.SoldProducts.Find(id);
            ISoldProductModel model = MapTheSoldProductObject(item);

            return model;
        }

        public List<ISoldProductModel> GetAll()
        {
            List<SoldProduct> list = _db.SoldProducts.ToList();
            List<ISoldProductModel> modelList = Factory.InstanceISoldProductModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheSoldProductObject(item));
            }

            return modelList;
        }

        public List<ISoldProductModel> GetByTable(int id)
        {
            List<SoldProduct> list = _db.SoldProducts.Where(x => x.TableID == id).ToList();
            List<ISoldProductModel> modelList = Factory.InstanceISoldProductModelList();

            foreach (var item in list)
            {               
                modelList.Add(MapTheSoldProductObject(item));
            }

            return modelList;
        }

        public List<ISoldProductModel> GetByCategory(int id)
        {
            List<SoldProduct> list = _db.SoldProducts.Where(x => x.CategoryID == id).ToList();
            List<ISoldProductModel> modelList = Factory.InstanceISoldProductModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheSoldProductObject(item));
            }

            return modelList;
        }

        public void Update(ISoldProductModel model)
        {
            var item = _db.SoldProducts.Find(model.ID);

            item.Price = model.Price;
            item.Detail = model.Detail;

            _db.SaveChanges();
        }

        private ISoldProductModel MapTheSoldProductObject(SoldProduct item)
        {
            ISoldProductModel model = Factory.InstanceSoldProductModel();

            model.ID = item.ID;
            model.Name = item.Name;
            model.CategoryID = item.CategoryID;
            model.Category.ID = item.CategoryID;
            model.Category.Name = _db.Categories.Where(x => x.ID == item.CategoryID).FirstOrDefault().Name;
            model.TableID = item.TableID;
            model.Table.ID = item.TableID;
            model.Price = item.Price;
            model.Detail = item.Detail;

            return model;
        }

    }
}