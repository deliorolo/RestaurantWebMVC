using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class SoldProductModel
    {

        public SoldProductModel()
        {
            Category = new CategoryModel();
            Table = new TableModel();
        }

        public int ID { get; set; }

        public int CategoryID { get; set; }

        public int TableID { get; set; }

        public string Name { get; set; }

        public CategoryModel Category { get; set; }

        public TableModel Table { get; set; }

        public decimal Price { get; set; }

        public string Detail { get; set; }
    }
}