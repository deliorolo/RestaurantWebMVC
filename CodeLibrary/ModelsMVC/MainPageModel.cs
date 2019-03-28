using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeLibrary.ModelsMVC
{
    public class MainPageModel : IMainPageModel
    {
        public MainPageModel()
        {

        }

        public MainPageModel(List<IAreaModel> areas, List<ITableModel> tables, List<ICategoryModel> categories, List<IProductModel> products)
        {
            Areas = areas;
            Tables = tables;
            Categories = categories;
            Products = products;
        }

        public List<IAreaModel> Areas { get; set; }

        public List<ITableModel> Tables { get; set; }

        public List<ICategoryModel> Categories { get; set; }

        public List<IProductModel> Products { get; set; }
    }
}