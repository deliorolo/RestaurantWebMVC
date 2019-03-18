using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class AreaDataAccess : IDataAccessRegular<AreaModel>
    {
        private RestaurantContext db = new RestaurantContext();

        public void Create(AreaModel model)
        {
            Area item = new Area();

            item.Name = model.Name;
            db.Areas.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = db.Areas.Find(id);
            db.Areas.Remove(item);
            db.SaveChanges();
        }

        public AreaModel Get(int id)
        {
            Area item = db.Areas.Find(id);
            AreaModel model = MapTheAreaObject(item);

            return model;
        }

        public List<AreaModel> GetAll()
        {
            List<Area> list = db.Areas.ToList();
            List<AreaModel> modelList = new List<AreaModel>();

            foreach (Area item in list)
            {
                modelList.Add(MapTheAreaObject(item));
            }

            return modelList;
        }

        public void Update(AreaModel model)
        {
            var item = db.Areas.Find(model.ID);

            item.Name = model.Name;
            db.SaveChanges();
        }

        private AreaModel MapTheAreaObject(Area item)
        {
            AreaModel model = new AreaModel();

            model.ID = item.ID;
            model.Name = item.Name;

            return model;
        }
    }
}