using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApiRestaurantDataAccess.Controllers
{
    public class ProductsController : ApiController
    {
        private IDataAccessSubCategory<IProductModel> productData = Factory.InstanceProductDataAccess();

        // GET: api/Products
        public List<IProductModel> Get()
        {
            return productData.GetAll();
        }

        // GET: api/Products/5
        public IProductModel Get(int id)
        {
            if (productData.GetAll().Exists(x => x.ID == id))
            {
                return productData.FindById(id);
            }
            else
            {
                return null;
            }
        }

        // POST: api/Products
        public void Post([FromBody]ProductModel product)
        {
            if (ModelState.IsValid)
            {
                if (productData.CheckIfAlreadyExist(product.Name) == false)
                {
                    IProductModel model = product;
                    productData.Create(model);
                }
            }
        }

        // PUT: api/Products/5
        public void Put([FromBody]ProductModel product)
        {
            if (ModelState.IsValid)
            {
                if (productData.CheckIfAlreadyExist(product.Name) == false ||
                    productData.FindById(product.ID).Name == product.Name)
                {
                    IProductModel model = product;
                    productData.Update(model);
                }
            }
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
            productData.Delete(id);
        }
    }
}
