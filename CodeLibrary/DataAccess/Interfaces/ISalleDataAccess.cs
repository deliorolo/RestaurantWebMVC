using CodeLibrary.ModelsMVC;
using System.Collections.Generic;

namespace CodeLibrary.DataAccess
{
    public interface ISaleDataAccess
    {
        /// <summary>
        /// It retrieves the list of all items from the database
        /// </summary>
        List<ISaleModel> GetSaleList();

        /// <summary>
        /// It deletes all items from the database on the Sales table
        /// </summary>
        void EraseDataFromSaleList();
    }
}