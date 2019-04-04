using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    public class CategoryModel : ICategoryModel
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(20, ErrorMessage = "Maximum of 20 characters field")]
        public string Name { get; set; }

        public int NumberOfProducts { get; set; } = 0;
    }
}