using System.Collections.Generic;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.EntityFramework.DataAccess
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
        void DeleteList(List<ISoldProductModel> list);
    }
}