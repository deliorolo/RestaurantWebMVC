using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class TableDataAccess : IDataAccessSubCategory<ITableModel>
    {
        private RestaurantContext db = new RestaurantContext();

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

            foreach (SoldProduct product in item.SoldProducts)
            {
                ISoldProductModel sp = new SoldProductModel();

                sp.ID = product.ID;
                sp.Name = product.Name;
                sp.Price = product.Price;
                sp.TableID = model.ID;
                sp.CategoryID = product.CategoryID;
                sp.Detail = product.Detail;
                sp.Category.ID = product.CategoryID;
                sp.Category.Name = product.Category.Name;

                model.SoldProducts.Add(sp);
            }

            return model;
        }

        public List<ITableModel> GetAll()
        {
            List<Table> list = db.Tables.ToList();
            List<ITableModel> modelList = new List<ITableModel>();

            foreach (var item in list)
            {
                modelList.Add(MapTheTableObject(item));
            }

            return modelList;
        }

        public List<ITableModel> GetBySubGroup(int id)
        {
            List<Table> list = db.Tables.Where(x => x.AreaID == id).ToList();
            List<ITableModel> modelList = new List<ITableModel>();

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
            ITableModel model = new TableModel();

            model.ID = item.ID;
            model.NumberOfTable = item.NumberOfTable;
            model.Occupied = item.Occupied;
            model.Area.ID = item.AreaID;
            model.AreaID = item.AreaID;
            model.Area.Name = item.Area.Name;

            return model;
        }
    }
}