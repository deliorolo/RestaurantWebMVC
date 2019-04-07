using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    public interface ITableModel
    {
        int ID { get; set; }

        [Display(Name = "Table")]
        int NumberOfTable { get; set; }

        int AreaID { get; set; }

        IAreaModel Area { get; set; }

        bool Occupied { get; set; }

        List<ISoldProductModel> SoldProducts { get; set; }

        string OrderSoldProducts { get; set; }
    }
}