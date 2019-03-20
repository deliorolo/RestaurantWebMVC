﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWeb.Services
{
    public interface IDataAccessSubCategory<T> : IDataAccessRegular<T>
    {
        List<T> GetBySubGroup(int id);
    }
}
