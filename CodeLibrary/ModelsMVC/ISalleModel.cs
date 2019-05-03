using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    public interface ISaleModel
    {
        [Display(Name = "Ammount")]
        int Ammount { get; set; }

        [Display(Name = "Category")]
        string CategoryName { get; set; }

        [Display(Name = "Product")]
        string Name { get; set; }

        [Display(Name = "Sum")]
        decimal Price { get; set; }
    }
}