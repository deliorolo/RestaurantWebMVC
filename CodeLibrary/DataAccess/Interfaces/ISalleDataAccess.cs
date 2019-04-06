using CodeLibrary.ModelsMVC;
using System.Collections.Generic;

namespace CodeLibrary.DataAccess
{
    public interface ISalleDataAccess
    {
        /// <summary>
        /// It retrieves the list of all items from the database
        /// </summary>
        List<ISalleModel> GetSalleList();

        /// <summary>
        /// It deletes all items from the database on the Salles table
        /// </summary>
        void EraseDataFromSalleList();
    }
}