using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class TableModel : ITableModel
    {
        public TableModel()
        {
            Area = new AreaModel();
            SoldProducts = new List<ISoldProductModel>();
        }

        public int ID { get; set; }

        public int AreaID { get; set; }

        public int NumberOfTable { get; set; }

        public IAreaModel Area { get; set; }

        public bool Occupied { get; set; } = false;

        public List<ISoldProductModel> SoldProducts { get; set; }
    }
}