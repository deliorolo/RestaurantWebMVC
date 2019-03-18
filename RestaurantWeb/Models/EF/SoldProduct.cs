using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models.EF
{
    public class SoldProduct
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public int TableID { get; set; }

        public decimal Price { get; set; }

        public string Detail { get; set; }


        public virtual Category Category { get; set; }

        public virtual Table Table { get; set; }
    }
}