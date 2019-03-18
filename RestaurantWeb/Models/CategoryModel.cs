using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class CategoryModel : ICategoryModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}