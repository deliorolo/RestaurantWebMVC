using System.Collections.Generic;

namespace CodeLibrary.ModelsMVC
{
    public interface IMainPageModel
    {
        List<IAreaModel> Areas { get; set; }
        List<ICategoryModel> Categories { get; set; }
        List<IProductModel> Products { get; set; }
        List<ITableModel> Tables { get; set; }
    }
}