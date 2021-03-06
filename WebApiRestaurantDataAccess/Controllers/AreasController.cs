﻿using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiRestaurantDataAccess.Controllers
{
    public class AreasController : ApiController
    {
        private IDataAccessRegular<IAreaModel> areaData = Factory.InstanceAreaDataAccess();

        // GET: api/Areas
        public List<IAreaModel> Get()
        {
            return areaData.GetAll();
        }

        // GET: api/Areas/5
        public IAreaModel Get(int id)
        {
            if (areaData.GetAll().Exists(x => x.ID == id))
            {
                return areaData.FindById(id);
            }
            else
            {
                return null;
            }
        }

        // POST: api/Areas
        public void Post([FromBody]AreaModel area)
        {
            if (ModelState.IsValid)
            {
                if (areaData.CheckIfAlreadyExist(area.Name) == false)
                {
                    IAreaModel model = area;
                    areaData.Create(model);
                }
            }
        }

        // PUT: api/Areas/5
        public void Put([FromBody]AreaModel area)
        {
            if (ModelState.IsValid)
            {
                if (areaData.CheckIfAlreadyExist(area.Name) == false)
                {
                    IAreaModel model = area;
                    areaData.Update(model);
                }
            }
        }

        // DELETE: api/Areas/5
        public void Delete(int id)
        {
            areaData.Delete(id);
        }
    }
}
