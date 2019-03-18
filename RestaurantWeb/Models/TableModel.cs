using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class TableModel
    {
        public TableModel()
        {
            Area = new AreaModel();
            SoldProducts = new List<SoldProductModel>();
        }

        public int ID { get; set; }

        public int AreaID { get; set; }

        public int NumberOfTable { get; set; }

        public AreaModel Area { get; set; }

        public bool Occupied { get; set; } = false;

        public List<SoldProductModel> SoldProducts { get; set; }
    }
}