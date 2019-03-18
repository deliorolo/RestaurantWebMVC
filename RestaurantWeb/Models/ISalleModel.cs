namespace RestaurantWeb.Models
{
    public interface ISalleModel
    {
        int Ammount { get; set; }
        string CategoryName { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
    }
}