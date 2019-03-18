using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class SalleModel
    {
        public int Ammount { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }
    }
}