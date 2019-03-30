using CodeLibrary.AccessoryCode;
using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class TableDataAccessWebAPI : IDataAccessSubCategory<ITableModel>
    {
        public bool CheckIfAlreadyExist(string name)
        {
            bool exists = false;

            if (GetAll().Where(x => x.NumberOfTable.ToString() == name).FirstOrDefault() != null)
            {
                exists = true;
            }

            return exists;
        }

        public void Create(ITableModel model)
        {
            using (var post = APIClientConfig.ApiClient.PostAsJsonAsync("tables", model))
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
            using (var delete = APIClientConfig.ApiClient.DeleteAsync($"tables/{id}"))
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

        public ITableModel FindById(int id)
        {
            using (var response = APIClientConfig.ApiClient.GetAsync($"tables/{id}"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<TableModel>();
                    readTask.Wait();

                    ITableModel model = readTask.Result;
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<ITableModel> GetAll()
        {
            using (var response = APIClientConfig.ApiClient.GetAsync("tables"))
            {
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readTask = response.Result.Content.ReadAsAsync<List<TableModel>>();
                    readTask.Wait();

                    List<ITableModel> model = readTask.Result.ToList<ITableModel>();
                    //TODO add sold products to model
                    return model;
                }
                else
                {
                    throw new Exception(response.Result.ReasonPhrase);
                }
            }
        }

        public List<ITableModel> GetBySubGroup(int id)
        {
            if (GetAll().Where(x => x.AreaID == id).ToList() != null)
            {
                return GetAll().Where(x => x.AreaID == id).ToList();
            }
            return null;
        }

        public void Update(ITableModel model)
        {
            using (var response = APIClientConfig.ApiClient.PutAsJsonAsync($"tables/{model.ID}", model))
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
