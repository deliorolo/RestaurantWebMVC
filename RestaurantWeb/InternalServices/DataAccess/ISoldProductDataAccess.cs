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
        List<ISoldProductModel> GetByTable(int id);
        List<ISoldProductModel> GetByCategory(int id);
        void Update(ISoldProductModel model);
    }
}