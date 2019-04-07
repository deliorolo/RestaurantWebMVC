using CodeLibrary.AccessoryCode;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodeLibrary.ModelsMVC
{
    // Table model represents each table identified by a number and with products on it
    public class TableModel : ITableModel
    {
        public TableModel()
        {

        }

        public TableModel(IAreaModel area, List<ISoldProductModel> soldProducts)
        {
            Area = area;
            SoldProducts = soldProducts;
        }

        public int ID { get; set; }

        public int AreaID { get; set; }

        [Required]
        [Display(Name = "Number of table")]
        [Range(1,999, ErrorMessage = "Maximum number of table is 99999")]
        public int NumberOfTable { get; set; }

        [JsonConverter(typeof(ConcreteConverter<AreaModel>))]
        public IAreaModel Area { get; set; }

        public bool Occupied { get; set; } = false;

        [JsonIgnore]
        public List<ISoldProductModel> SoldProducts { get; set; }
    }
}