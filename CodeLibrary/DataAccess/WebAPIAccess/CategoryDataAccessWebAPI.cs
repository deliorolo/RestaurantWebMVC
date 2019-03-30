using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class CategoryDataAccessWebAPI : IDataAccessRegular<ICategoryModel>
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

        public void Create(ICategoryModel model)
        {
            using (var post = APIClientConfig.ApiClient.PostAsJsonAsync("categories", model))
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
            using (var delete = APIClientConfig.ApiClient.DeleteAsync($"categories/{id}"))
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

        public ICategoryModel FindById(int id)
        {
            using (var response = APIClientConfig.ApiClient.GetAsync($"categories/{id}"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<CategoryModel>();
                    readTask.Wait();

                    ICategoryModel model = readTask.Result;
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<ICategoryModel> GetAll()
        {
            using (var response = APIClientConfig.ApiClient.GetAsync("categories"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<List<CategoryModel>>();
                    readTask.Wait();

                    List<ICategoryModel> model = readTask.Result.ToList<ICategoryModel>();
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public void Update(ICategoryModel model)
        {
            using (var response = APIClientConfig.ApiClient.PutAsJsonAsync($"categories/{model.ID}", model))
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
