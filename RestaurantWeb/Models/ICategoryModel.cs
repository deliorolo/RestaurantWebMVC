using System.ComponentModel.DataAnnotations;

namespace RestaurantWeb.Models
{
    public interface ICategoryModel
    {
        int ID { get; set; }

        [Display(Name = "Category")]
        string Name { get; set; }
    }
}