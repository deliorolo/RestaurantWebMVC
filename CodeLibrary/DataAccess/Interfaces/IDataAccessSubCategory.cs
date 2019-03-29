using System.Collections.Generic;

namespace CodeLibrary.DataAccess
{
    public interface IDataAccessSubCategory<T> : IDataAccessRegular<T>
    {
        List<T> GetBySubGroup(int id);
    }
}
