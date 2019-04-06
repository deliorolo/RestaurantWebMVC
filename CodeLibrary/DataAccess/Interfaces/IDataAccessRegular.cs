using System.Collections.Generic;

namespace CodeLibrary.DataAccess
{
    public interface IDataAccessRegular<T>
    {
        /// <summary>
        /// It retrieves the list of all items from the database
        /// </summary>
        List<T> GetAll();

        /// <summary>
        /// It retrieves an item from the database based on ID
        /// </summary>
        T FindById(int id);

        /// <summary>
        /// It deletes an item from the database
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// It edits an existing item from the database
        /// </summary>
        void Update(T model);

        /// <summary>
        /// It adds a new item to the database
        /// </summary>
        void Create(T model);

        /// <summary>
        /// It checks if this item already exists in the database (case insensitive)
        /// </summary>
        bool CheckIfAlreadyExist(string name);
    }
}
