using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    public interface IAreaModel
    {
        int ID { get; set; }

        [Display(Name = "Area")]
        string Name { get; set; }

        int NumberOfTables { get; set; }
    }
}