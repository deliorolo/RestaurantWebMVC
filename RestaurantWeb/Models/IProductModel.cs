using System.ComponentModel.DataAnnotations;

namespace RestaurantWeb.Models
{
    public interface IProductModel
    {
        ICategoryModel Category { get; set; }
        int CategoryID { get; set; }
        int ID { get; set; }

        [Display(Name = "Product")]
        string Name { get; set; }

        [Display(Name = "Price")]
        decimal Price { get; set; }
    }
}