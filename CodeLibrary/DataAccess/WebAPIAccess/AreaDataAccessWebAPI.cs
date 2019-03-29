using CodeLibrary.EntityFramework;
using CodeLibrary.ModelsMVC;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeLibrary.DataAccess.WebAPIAccess
{
    class AreaDataAccessWebAPI : IDataAccessRegular<IAreaModel>
    {
        public AreaDataAccessWebAPI()
        {

        }

        public bool CheckIfAlreadyExist(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(IAreaModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void Update(IAreaModel model)
        {
            throw new NotImplementedException();
        }
    }
}
