using System.Collections.Generic;

namespace CodeLibrary.DataAccess
{
    public interface IDataAccessSubCategory<T> : IDataAccessRegular<T>
    {
        /// <summary>
        /// It retrieves a list from the database based on the group ID
        /// </summary>
        List<T> GetBySubGroup(int id);
    }
}
