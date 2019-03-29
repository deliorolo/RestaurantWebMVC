namespace CodeLibrary.EntityFramework.Models
{
    public class SoldProduct
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public int TableID { get; set; }

        public decimal Price { get; set; }

        public string Detail { get; set; }


        public virtual Category Category { get; set; }

        public virtual Table Table { get; set; }
    }
}