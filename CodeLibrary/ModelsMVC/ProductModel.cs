using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeLibrary.ModelsMVC
{
    public class ProductModel : IProductModel
    {
        public ProductModel()
        {

        }

        public ProductModel(ICategoryModel category)
        {
            Category = category;
        }

        public int ID { get; set; }

        public int CategoryID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(20, ErrorMessage = "Maximum of 20 characters field")]
        public string Name { get; set; }

        public ICategoryModel Category { get; set; }

        [Required]
        [Display(Name = "Price (€)")]
        [Range(0.05, 9999, ErrorMessage = "Please enter a valid price ex: 1,50")]
        public decimal Price { get; set; }
    }
}