using System.Collections.Generic;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess
{
    public interface ISoldProductAccomplishedDataAccess
    {
        /// <summary>
        /// It adds a new item to the database
        /// </summary>
        void Create(ISoldProductAccomplishedModel model);

        /// <summary>
        /// It deletes an item from the database
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// It retrieves an item from the database based on ID
        /// </summary>
        ISoldProductAccomplishedModel FindById(int id);

        /// <summary>
        /// It retrieves the list of all items from the database
        /// </summary>
        List<ISoldProductAccomplishedModel> GetAll();

        /// <summary>
        /// It retrieves a list from the database based on the category ID
        /// </summary>
        List<ISoldProductAccomplishedModel> GetByCategory(int id);
    }
}