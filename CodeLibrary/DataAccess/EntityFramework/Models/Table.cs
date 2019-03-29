using System.Collections.Generic;

namespace CodeLibrary.EntityFramework.Models
{
    public class Table
    {
        public int ID { get; set; }

        public int NumberOfTable { get; set; }

        public int AreaID { get; set; }

        public bool Occupied { get; set; } = false;


        public virtual Area Area { get; set; }

        public virtual ICollection<SoldProduct> SoldProducts { get; set; }
    }
}