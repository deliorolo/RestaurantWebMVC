using System.Collections.Generic;

namespace CodeLibrary.ModelsMVC
{
    // Main Page model is used in order to merge to a single model data from different models to be used in the views
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