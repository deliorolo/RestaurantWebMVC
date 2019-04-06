using System.Collections.Generic;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.DataAccess
{
    public interface ISoldProductDataAccess
    {
        /// <summary>
        /// It adds a new item to the database
        /// </summary>
        void Create(ISoldProductModel model);

        /// <summary>
        /// It deletes an item from the database
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// It retrieves an item from the database based on ID
        /// </summary>
        ISoldProductModel FindById(int id);

        /// <summary>
        /// It retrieves the list of all items from the database
        /// </summary>
        List<ISoldProductModel> GetAll();

        /// <summary>
        /// It retrieves a list from the database based on the table ID
        /// </summary>
        List<ISoldProductModel> GetByTable(int id);

        /// <summary>
        /// It retrieves a list from the database based on the category ID
        /// </summary>
        List<ISoldProductModel> GetByCategory(int id);

        /// <summary>
        /// It edits an existing item from the database
        /// </summary>
        void Update(ISoldProductModel model);

        /// <summary>
        /// It deletes a list of items from the database
        /// </summary>
        void DeleteList(List<ISoldProductModel> list);
    }
}