using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWeb.Services
{
    public interface IDataAccessRegular<T>
    {
        List<T> GetAll();
        T FindById(int id);
        void Delete(int id);
        void Update(T model);
        void Create(T model);
        bool CheckIfAlreadyExist(string name);
    }
}
