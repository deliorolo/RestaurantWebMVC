using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    // Area model represents a group that contains tables of the same location on it
    public class AreaModel : IAreaModel
    {
        public int ID { get; set; }

        [Required]
        [Display (Name = "Name")]
        [StringLength(20, ErrorMessage = "Maximum of 20 characters field")]
        public string Name { get; set; }

        public int NumberOfTables { get; set; } = 0;
    }
}