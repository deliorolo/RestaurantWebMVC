using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class ProductDataAccessWebAPI : IDataAccessSubCategory<IProductModel>
    {
        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (GetAll().Where(x => x.Name == name).FirstOrDefault() != null)
            {
                exists = true;
            }

            return exists;
        }

        public void Create(IProductModel model)
        {
            using (var post = APIClientConfig.ApiClient.PostAsJsonAsync("products", model))
            {
                post.Wait();

                if (post.Result.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(post.Result.ReasonPhrase);
                }
            }
        }

        public void Delete(int id)
        {
            using (var delete = APIClientConfig.ApiClient.DeleteAsync($"products/{id}"))
            {
                delete.Wait();

                if (delete.Result.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(delete.Result.ReasonPhrase);
                }
            }
        }

        public IProductModel FindById(int id)
        {
            using (var response = APIClientConfig.ApiClient.GetAsync($"products/{id}"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<ProductModel>();
                    readTask.Wait();

                    IProductModel model = readTask.Result;
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<IProductModel> GetAll()
        {
            using (var response = APIClientConfig.ApiClient.GetAsync("products"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<List<ProductModel>>();
                    readTask.Wait();

                    List<IProductModel> model = readTask.Result.ToList<IProductModel>();                  

                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<IProductModel> GetBySubGroup(int id)
        {
            if(GetAll().Where(x => x.CategoryID == id).ToList() != null)
            {
                return GetAll().Where(x => x.CategoryID == id).ToList();
            }
            return null;
        }

        public void Update(IProductModel model)
        {
            using (var response = APIClientConfig.ApiClient.PutAsJsonAsync($"products/{model.ID}", model))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {

                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }
    }
}
