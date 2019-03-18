using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class ProductModel
    {
        public ProductModel()
        {
            Category = new CategoryModel();
        }

        public int ID { get; set; }

        public int CategoryID { get; set; }

        public string Name { get; set; }

        public CategoryModel Category { get; set; }

        public decimal Price { get; set; }
    }
}