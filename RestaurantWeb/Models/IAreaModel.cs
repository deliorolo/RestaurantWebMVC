using System.ComponentModel.DataAnnotations;

namespace RestaurantWeb.Models
{
    public interface IAreaModel
    {
        int ID { get; set; }

        [Display(Name = "Area")]
        string Name { get; set; }

        int NumberOfTables { get; set; }
    }
}