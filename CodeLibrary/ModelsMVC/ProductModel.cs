using CodeLibrary.AccessoryCode;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    // Product model represents a individual product that is created by user and it is ready to be added to tables
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

        [JsonConverter(typeof(ConcreteConverter<CategoryModel>))]
        public ICategoryModel Category { get; set; }

        [Required]
        [Display(Name = "Price (€)")]
        [Range(0.05, 9999, ErrorMessage = "Please enter a valid price ex: 1,50")]
        public decimal Price { get; set; }
    }
}