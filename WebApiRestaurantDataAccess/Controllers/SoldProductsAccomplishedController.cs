using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiRestaurantDataAccess.Controllers
{
    public class SoldProductsAccomplishedController : ApiController
    {
        private ISoldProductAccomplishedDataAccess soldProductAccomplishedData = Factory.InstanceSoldProductAccomplishedDataAccess();

        // GET: api/SoldProductsAccomplished
        public List<ISoldProductAccomplishedModel> Get()
        {
            return soldProductAccomplishedData.GetAll();
        }

        // GET: api/SoldProductsAccomplished/5
        public ISoldProductAccomplishedModel Get(int id)
        {
            if (soldProductAccomplishedData.GetAll().Exists(x => x.ID == id))
            {
                return soldProductAccomplishedData.FindById(id);
            }
            else
            {
                return null;
            }
        }

        // POST: api/SoldProductsAccomplished
        public void Post([FromBody]SoldProductAccomplishedModel product)
        {
            //if (ModelState.IsValid)
            //{
                ISoldProductAccomplishedModel model = product;
                soldProductAccomplishedData.Create(model);
            //}
        }

        // PUT: api/SoldProductsAccomplished/5
        //public void Put([FromBody]SoldProductAccomplishedModel product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ISoldProductAccomplishedModel model = product;
        //        soldProductAccomplishedData.Update(model);
        //    }
        //}

        // DELETE: api/SoldProductsAccomplished/5
        public void Delete(int id)
        {
            soldProductAccomplishedData.Delete(id);
        }
    }
}
