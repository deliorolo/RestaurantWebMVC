using System.Collections.Generic;
using RestaurantWeb.Models;

namespace RestaurantWeb.InternalServices
{
    public interface ISoldProductDataAccess
    {
        void Create(ISoldProductModel model);
        void Delete(int id);
        ISoldProductModel FindById(int id);
        List<ISoldProductModel> GetAll();
        List<ISoldProductModel> GetBySubGroup(int id);
        void Update(ISoldProductModel model);
    }
}