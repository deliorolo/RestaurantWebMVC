﻿using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Services
{
    public class AreaDataAccess : IDataAccessRegular<IAreaModel>
    {
        private RestaurantContext db = new RestaurantContext();

        public void Create(IAreaModel model)
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

        public IAreaModel Get(int id)
        {
            Area item = db.Areas.Find(id);
            IAreaModel model = MapTheAreaObject(item);

            return model;
        }

        public List<IAreaModel> GetAll()
        {
            List<Area> list = db.Areas.ToList();
            List<IAreaModel> modelList = new List<IAreaModel>();

            foreach (Area item in list)
            {
                modelList.Add(MapTheAreaObject(item));
            }

            return modelList;
        }

        public void Update(IAreaModel model)
        {
            var item = db.Areas.Find(model.ID);

            item.Name = model.Name;
            db.SaveChanges();
        }

        private IAreaModel MapTheAreaObject(Area item)
        {
            IAreaModel model = new AreaModel();

            model.ID = item.ID;
            model.Name = item.Name;

            return model;
        }
    }
}