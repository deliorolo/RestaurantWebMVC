using RestaurantWeb.AccessoryCode;
using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.InternalServices
{
    public class TableDataAccess : IDataAccessSubCategory<ITableModel>
    {
        private RestaurantContext db = Factory.InstanceRestaurantContext();

        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (db.Tables.Where(x => x.NumberOfTable.ToString() == name).FirstOrDefault() != null)
            {
                exists = true;
            }

            return exists;
        }

        public void Create(ITableModel model)
        {
            Table item = new Table();

            item.NumberOfTable = model.NumberOfTable;
            item.AreaID = model.AreaID;
            item.Occupied = false;
            db.Tables.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.Tables.Find(id);
            db.Tables.Remove(item);
            db.SaveChanges();
        }

        public ITableModel FindById(int id)
        {
            Table item = db.Tables.Find(id);
            ITableModel model = MapTheTableObject(item);

            return model;
        }

        public List<ITableModel> GetAll()
        {
            List<Table> list = db.Tables.ToList();
            List<ITableModel> modelList = Factory.InstanceITableModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheTableObject(item));
            }

            return modelList;
        }

        public List<ITableModel> GetBySubGroup(int id)
        {
            List<Table> list = db.Tables.Where(x => x.AreaID == id).ToList();
            List<ITableModel> modelList = Factory.InstanceITableModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheTableObject(item));
            }

            return modelList;
        }

        public void Update(ITableModel model)
        {
            var item = db.Tables.Find(model.ID);

            item.NumberOfTable = model.NumberOfTable;
            item.AreaID = model.AreaID;
            item.Occupied = model.Occupied;

            db.SaveChanges();
        }

        private ITableModel MapTheTableObject(Table item)
        {
            ITableModel model = Factory.InstanceTableModel();

            model.ID = item.ID;
            model.NumberOfTable = item.NumberOfTable;
            model.Occupied = item.Occupied;
            model.Area.ID = item.AreaID;
            model.AreaID = item.AreaID;
            model.Area.Name = item.Area.Name;

            foreach (SoldProduct product in item.SoldProducts)
            {
                ISoldProductModel soldProduct = Factory.InstanceSoldProductModel();

                soldProduct.ID = product.ID;
                soldProduct.Name = product.Name;
                soldProduct.Price = product.Price;
                soldProduct.TableID = product.TableID;
                soldProduct.CategoryID = product.CategoryID;
                soldProduct.Category.ID = product.CategoryID;
                soldProduct.Category.Name = product.Category.Name;
                soldProduct.Detail = product.Detail;
                soldProduct.Table = model;

                model.SoldProducts.Add(soldProduct);
            }

            return model;
        }
    }
}