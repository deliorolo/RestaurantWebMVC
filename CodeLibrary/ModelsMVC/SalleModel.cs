using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    // Salle model is used for better vizualization of the complete sold products accomplished during all day
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