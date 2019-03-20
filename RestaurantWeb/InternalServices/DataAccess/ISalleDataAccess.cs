using System.Collections.Generic;
using RestaurantWeb.Models;

namespace RestaurantWeb.InternalServices
{
    public interface ISalleDataAccess
    {
        List<ISalleModel> GetSalleList();

        void EraseDataFromSalleList();
    }
}