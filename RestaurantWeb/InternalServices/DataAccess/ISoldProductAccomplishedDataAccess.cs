using System.Collections.Generic;
using RestaurantWeb.Models;

namespace RestaurantWeb.InternalServices
{
    public interface ISoldProductAccomplishedDataAccess
    {
        void Create(ISoldProductAccomplishedModel model);
        void Delete(int id);
        ISoldProductAccomplishedModel FindById(int id);
        List<ISoldProductAccomplishedModel> GetAll();
        List<ISoldProductAccomplishedModel> GetByCategory(int id);
    }
}