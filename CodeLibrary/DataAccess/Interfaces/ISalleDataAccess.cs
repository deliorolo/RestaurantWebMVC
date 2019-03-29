using CodeLibrary.ModelsMVC;
using System.Collections.Generic;

namespace CodeLibrary.DataAccess
{
    public interface ISalleDataAccess
    {
        List<ISalleModel> GetSalleList();

        void EraseDataFromSalleList();
    }
}