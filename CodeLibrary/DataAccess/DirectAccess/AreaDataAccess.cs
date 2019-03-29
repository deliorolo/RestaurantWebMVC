using System.Collections.Generic;
using System.Linq;
using CodeLibrary.AccessoryCode;
using CodeLibrary.EntityFramework;
using CodeLibrary.EntityFramework.Models;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess
{
    public class AreaDataAccess : IDataAccessRegular<IAreaModel>
    {
        private RestaurantContext _db;

        public AreaDataAccess(RestaurantContext db)
        {
            _db = db;
        }

        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (_db.Areas.Where(x => x.Name == name).FirstOrDefault() != null)
            {
                exists = true;
            }

            return exists;
        }

        public void Create(IAreaModel model)
        {
            Area item = new Area()
            {
                Name = model.Name
            };
            _db.Areas.Add(item);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _db.Areas.Find(id);
            _db.Areas.Remove(item);
            _db.SaveChanges();
        }

        public IAreaModel FindById(int id)
        {
            Area item = _db.Areas.Find(id);
            IAreaModel model = MapTheAreaObject(item);

            return model;
        }

        public List<IAreaModel> GetAll()
        {
            List<Area> list = _db.Areas.ToList();
            List<IAreaModel> modelList = Factory.InstanceIAreaModelList();

            foreach (Area item in list)
            {
                modelList.Add(MapTheAreaObject(item));
            }

            return modelList;
        }

        public void Update(IAreaModel model)
        {
            var item = _db.Areas.Find(model.ID);

            item.Name = model.Name;
            _db.SaveChanges();
        }

        private IAreaModel MapTheAreaObject(Area item)
        {
            IAreaModel model = Factory.InstanceAreaModel();

            model.ID = item.ID;
            model.Name = item.Name;

            return model;
        }
    }
}