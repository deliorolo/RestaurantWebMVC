using CodeLibrary.AccessoryCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeLibrary.ModelsMVC
{
    public class SoldProductModel : ISoldProductModel
    {
        public SoldProductModel()
        {

        }

        public SoldProductModel(ICategoryModel category, ITableModel table)
        {
            Category = category;
            Table = table;
        }

        public int ID { get; set; }

        public int CategoryID { get; set; }

        public int TableID { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(ConcreteConverter<CategoryModel>))]
        public ICategoryModel Category { get; set; }

        [JsonConverter(typeof(ConcreteConverter<TableModel>))]
        public ITableModel Table { get; set; }

        [Required]
        [Display(Name = "Price €")]
        [Range(0.05, 9999, ErrorMessage = "Please enter a valid price ex: 1.50")]
        public decimal Price { get; set; }

        [Display(Name = "Detail")]
        [StringLength(20, ErrorMessage = "Maximum of 20 characters field")]
        public string Detail { get; set; }
    }
}