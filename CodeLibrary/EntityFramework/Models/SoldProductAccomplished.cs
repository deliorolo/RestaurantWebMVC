namespace CodeLibrary.EntityFramework.Models
{
    public class SoldProductAccomplished
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public decimal Price { get; set; }


        public virtual Category Category { get; set; }
    }
}