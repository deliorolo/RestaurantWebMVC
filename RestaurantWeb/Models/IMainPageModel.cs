using System.Collections.Generic;

namespace RestaurantWeb.Models
{
    public interface IMainPageModel
    {
        List<IAreaModel> Areas { get; set; }
        List<ICategoryModel> Categories { get; set; }
        List<IProductModel> Products { get; set; }
        List<ITableModel> Tables { get; set; }
    }
}