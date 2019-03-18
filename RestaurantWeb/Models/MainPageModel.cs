using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class MainPageModel
    {
        public List<AreaModel> Areas { get; set; }

        public List<TableModel> Tables { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<ProductModel> Products { get; set; }
    }
}