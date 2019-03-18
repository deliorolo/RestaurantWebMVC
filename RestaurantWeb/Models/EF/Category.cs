using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantWeb.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}