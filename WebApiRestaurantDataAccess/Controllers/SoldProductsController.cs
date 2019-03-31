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
    public class SoldProductsController : ApiController
    {
        private ISoldProductDataAccess soldProductData = Factory.InstanceSoldProductDataAccess();

        // GET: api/SoldProducts
        public List<ISoldProductModel> Get()
        {
            return soldProductData.GetAll();
        }

        // GET: api/SoldProducts/5
        public ISoldProductModel Get(int id)
        {
            if (soldProductData.GetAll().Exists(x => x.ID == id))
            {
                return soldProductData.FindById(id);
            }
            else
            {
                return null;
            }
        }

        // POST: api/SoldProducts
        public void Post([FromBody]SoldProductModel product)
        {
            //if (ModelState.IsValid)
            //{
                ISoldProductModel model = product;
                soldProductData.Create(model);
            //}
        }

        // PUT: api/SoldProducts/5
        public void Put([FromBody]SoldProductModel product)
        {
            if (ModelState.IsValid)
            {
                ISoldProductModel model = product;
                soldProductData.Update(model);
            }
        }

        // DELETE: api/SoldProducts/5
        public void Delete(int id)
        {
            soldProductData.Delete(id);
        }
    }
}
