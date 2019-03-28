using System.Collections.Generic;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.EntityFramework.DataAccess
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