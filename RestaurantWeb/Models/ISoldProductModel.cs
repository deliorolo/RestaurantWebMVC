namespace RestaurantWeb.Models
{
    public interface ISoldProductModel
    {
        ICategoryModel Category { get; set; }
        int CategoryID { get; set; }
        string Detail { get; set; }
        int ID { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        ITableModel Table { get; set; }
        int TableID { get; set; }
    }
}