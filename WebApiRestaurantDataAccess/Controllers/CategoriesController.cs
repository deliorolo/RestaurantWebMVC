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
    public class CategoriesController : ApiController
    {
        private IDataAccessRegular<ICategoryModel> categoryData = Factory.InstanceCategoryDataAccess();

        // GET: api/Categories
        public List<ICategoryModel> Get()
        {
            return categoryData.GetAll();
        }

        // GET: api/Categories/5
        public ICategoryModel Get(int id)
        {
            if (categoryData.GetAll().Exists(x => x.ID == id))
            {
                return categoryData.FindById(id);
            }
            else
            {
                return null;
            }
        }

        // POST: api/Categories
        public void Post([FromBody]CategoryModel area)
        {
            if (ModelState.IsValid)
            {
                if (categoryData.CheckIfAlreadyExist(area.Name) == false)
                {
                    ICategoryModel model = area;
                    categoryData.Create(model);
                }
            }
        }

        // PUT: api/Categories/5
        public void Put([FromBody]CategoryModel area)
        {
            if (ModelState.IsValid)
            {
                if (categoryData.CheckIfAlreadyExist(area.Name) == false)
                {
                    ICategoryModel model = area;
                    categoryData.Update(model);
                }
            }
        }

        // DELETE: api/Categories/5
        public void Delete(int id)
        {
            categoryData.Delete(id);
        }
    }
}
