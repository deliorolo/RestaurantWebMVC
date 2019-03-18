using System.Collections.Generic;
using RestaurantWeb.Models;

namespace RestaurantWeb.Services
{
    public interface ISalleDataAccess
    {
        List<ISalleModel> GetSalleList();

        void EraseDataFromSalleList();
    }
}