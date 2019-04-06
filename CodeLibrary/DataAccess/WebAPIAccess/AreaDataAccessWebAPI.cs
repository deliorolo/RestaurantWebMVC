using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class AreaDataAccessWebAPI : IDataAccessRegular<IAreaModel>
    {
        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (GetAll().Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault() != null)
            {
                exists = true;
            }

            return exists;
        }

        public void Create(IAreaModel model)
        {
            using (var post = APIClientConfig.ApiClient.PostAsJsonAsync("areas", model))
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
            using (var delete = APIClientConfig.ApiClient.DeleteAsync($"areas/{id}"))
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

        public IAreaModel FindById(int id)
        {            
            using (var response = APIClientConfig.ApiClient.GetAsync($"areas/{id}"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<AreaModel>();
                    readTask.Wait();

                    IAreaModel model = readTask.Result;
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<IAreaModel> GetAll()
        {
            using (var response = APIClientConfig.ApiClient.GetAsync("areas"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<List<AreaModel>>();
                    readTask.Wait();

                    List<IAreaModel> model = readTask.Result.ToList<IAreaModel>();
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public void Update(IAreaModel model)
        {
            using (var response = APIClientConfig.ApiClient.PutAsJsonAsync($"areas/{model.ID}", model))
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
