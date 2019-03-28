using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    public interface ITableModel
    {
        IAreaModel Area { get; set; }
        int AreaID { get; set; }
        int ID { get; set; }

        [Display(Name = "Table")]
        int NumberOfTable { get; set; }

        bool Occupied { get; set; }
        List<ISoldProductModel> SoldProducts { get; set; }
    }
}