//using RestaurantWeb.Models.EF;
using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework.Models;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.EntityFramework.DataAccess
{
    public class TableDataAccess : IDataAccessSubCategory<ITableModel>
    {
        private RestaurantContext _db;

        public TableDataAccess(RestaurantContext db)
        {
            _db = db;
        }

        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (_db.Tables.Where(x => x.NumberOfTable.ToString() == name).FirstOrDefault() != null)
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
            _db.Tables.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _db.Tables.Find(id);
            _db.Tables.Remove(item);
            _db.SaveChanges();
        }

        public ITableModel FindById(int id)
        {
            Table item = _db.Tables.Find(id);
            ITableModel model = MapTheTableObject(item);

            return model;
        }

        public List<ITableModel> GetAll()
        {
            List<Table> list = _db.Tables.ToList();
            List<ITableModel> modelList = Factory.InstanceITableModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheTableObject(item));
            }

            return modelList;
        }

        public List<ITableModel> GetBySubGroup(int id)
        {
            List<Table> list = _db.Tables.Where(x => x.AreaID == id).ToList();
            List<ITableModel> modelList = Factory.InstanceITableModelList();

            foreach (var item in list)
            {
                modelList.Add(MapTheTableObject(item));
            }

            return modelList;
        }

        public void Update(ITableModel model)
        {
            var item = _db.Tables.Find(model.ID);

            item.NumberOfTable = model.NumberOfTable;
            item.AreaID = model.AreaID;
            item.Occupied = model.Occupied;

            _db.SaveChanges();
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