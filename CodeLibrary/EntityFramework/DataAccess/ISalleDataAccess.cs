using CodeLibrary.ModelsMVC;
using System.Collections.Generic;

namespace CodeLibrary.EntityFramework.DataAccess
{
    public interface ISalleDataAccess
    {
        List<ISalleModel> GetSalleList();

        void EraseDataFromSalleList();
    }
}