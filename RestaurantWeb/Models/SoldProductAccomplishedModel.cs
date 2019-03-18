using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class SoldProductAccomplishedModel
    {
        public SoldProductAccomplishedModel()
        {
            Category = new CategoryModel();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public decimal Price { get; set; }

        public CategoryModel Category { get; set; }
    }
}