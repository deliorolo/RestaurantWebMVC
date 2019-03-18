using System.Collections.Generic;

namespace RestaurantWeb.Models
{
    public interface ITableModel
    {
        IAreaModel Area { get; set; }
        int AreaID { get; set; }
        int ID { get; set; }
        int NumberOfTable { get; set; }
        bool Occupied { get; set; }
        List<ISoldProductModel> SoldProducts { get; set; }
    }
}