using System.ComponentModel.DataAnnotations;

namespace RestaurantWeb.Models
{
    public interface ISoldProductModel
    {
        ICategoryModel Category { get; set; }
        int CategoryID { get; set; }

        [Display(Name = "Detail")]
        string Detail { get; set; }
        int ID { get; set; }

        [Display(Name = "Product")]
        string Name { get; set; }

        [Display(Name = "Price")]
        decimal Price { get; set; }
        ITableModel Table { get; set; }
        int TableID { get; set; }
    }
}