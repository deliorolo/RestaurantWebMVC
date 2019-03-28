using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeLibrary.ModelsMVC
{
    public class SalleModel : ISalleModel
    {
        [Display(Name = "Ammount")]
        public int Ammount { get; set; }

        [Display(Name = "Product")]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Sum")]
        public decimal Price { get; set; }
    }
}