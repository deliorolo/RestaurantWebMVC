using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models.EF
{
    public class SoldProductAccomplished
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public decimal Price { get; set; }


        public virtual Category Category { get; set; }
    }
}