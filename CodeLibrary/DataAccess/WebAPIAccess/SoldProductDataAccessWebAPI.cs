using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CodeLibrary.AccessoryCode;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class SoldProductDataAccessWebAPI : ISoldProductDataAccess
    {
        public void Create(ISoldProductModel model)
        {
            using (var post = APIClientConfig.ApiClient.PostAsJsonAsync("soldproducts", model))
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
            using (var delete = APIClientConfig.ApiClient.DeleteAsync($"soldproducts/{id}"))
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

        public void DeleteList(List<ISoldProductModel> list)
        {
            foreach (var item in list)
            {
                Delete(item.ID);
            }
        }

        public ISoldProductModel FindById(int id)
        {
            using (var response = APIClientConfig.ApiClient.GetAsync($"soldproducts/{id}"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<SoldProductModel>();
                    readTask.Wait();

                    ISoldProductModel model = readTask.Result;
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<ISoldProductModel> GetAll()
        {
            using (var response = APIClientConfig.ApiClient.GetAsync("soldproducts"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<List<SoldProductModel>>();
                    readTask.Wait();

                    List<ISoldProductModel> model = readTask.Result.ToList<ISoldProductModel>();

                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<ISoldProductModel> GetByCategory(int id)
        {
            if (GetAll().Where(x => x.CategoryID == id).ToList() != null)
            {
                return GetAll().Where(x => x.CategoryID == id).ToList();
            }
            return Factory.InstanceISoldProductModelList();
        }

        public List<ISoldProductModel> GetByTable(int id)
        {
            if (GetAll().Where(x => x.TableID == id).ToList() != null)
            {
                return GetAll().Where(x => x.TableID == id).ToList();
            }
            return Factory.InstanceISoldProductModelList();
        }

        public void Update(ISoldProductModel model)
        {
            using (var response = APIClientConfig.ApiClient.PutAsJsonAsync($"soldproducts/{model.ID}", model))
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
