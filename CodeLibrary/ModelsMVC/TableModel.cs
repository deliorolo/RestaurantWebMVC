﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeLibrary.ModelsMVC
{
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

        public IAreaModel Area { get; set; }

        public bool Occupied { get; set; } = false;

        public List<ISoldProductModel> SoldProducts { get; set; }
    }
}