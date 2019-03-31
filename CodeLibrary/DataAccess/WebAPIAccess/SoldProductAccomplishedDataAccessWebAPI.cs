using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CodeLibrary.AccessoryCode;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class SoldProductAccomplishedDataAccessWebAPI : ISoldProductAccomplishedDataAccess
    {
        public void Create(ISoldProductAccomplishedModel model)
        {
            using (var post = APIClientConfig.ApiClient.PostAsJsonAsync("soldproductsaccomplished", model))
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
            using (var delete = APIClientConfig.ApiClient.DeleteAsync($"soldproductsaccomplished/{id}"))
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

        public ISoldProductAccomplishedModel FindById(int id)
        {
            using (var response = APIClientConfig.ApiClient.GetAsync($"soldproductsaccomplished/{id}"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<SoldProductAccomplishedModel>();
                    readTask.Wait();

                    ISoldProductAccomplishedModel model = readTask.Result;
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<ISoldProductAccomplishedModel> GetAll()
        {
            using (var response = APIClientConfig.ApiClient.GetAsync("soldproductsaccomplished"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<List<SoldProductAccomplishedModel>>();
                    readTask.Wait();

                    List<ISoldProductAccomplishedModel> model = readTask.Result.ToList<ISoldProductAccomplishedModel>();

                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<ISoldProductAccomplishedModel> GetByCategory(int id)
        {
            if (GetAll().Where(x => x.CategoryID == id).ToList() != null)
            {
                return GetAll().Where(x => x.CategoryID == id).ToList();
            }
            return Factory.InstanceISoldProductAccomplishedModelList();
        }
    }
}
