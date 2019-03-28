using System.Collections.Generic;

namespace CodeLibrary.EntityFramework.DataAccess
{
    public interface IDataAccessSubCategory<T> : IDataAccessRegular<T>
    {
        List<T> GetBySubGroup(int id);
    }
}
