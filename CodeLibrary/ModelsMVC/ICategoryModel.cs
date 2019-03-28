using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    public interface ICategoryModel
    {
        int ID { get; set; }

        [Display(Name = "Category")]
        string Name { get; set; }

        int NumberOfProducts { get; set; }
    }
}