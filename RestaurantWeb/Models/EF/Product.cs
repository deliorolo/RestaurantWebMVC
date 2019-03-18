using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public decimal Price { get; set; }


        public virtual Category Category { get; set; }
    }
}