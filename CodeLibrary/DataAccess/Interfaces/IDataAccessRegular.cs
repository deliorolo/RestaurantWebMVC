using System.Collections.Generic;

namespace CodeLibrary.DataAccess
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
