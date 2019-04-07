using CodeLibrary.AccessoryCode;
using Newtonsoft.Json;

namespace CodeLibrary.ModelsMVC
{
    // Sold product accomplished model represents products that were alredy paid from the tables
    public class SoldProductAccomplishedModel : ISoldProductAccomplishedModel
    {
        public SoldProductAccomplishedModel()
        {

        }

        public SoldProductAccomplishedModel(ICategoryModel category)
        {
            Category = category;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public decimal Price { get; set; }

        [JsonConverter(typeof(ConcreteConverter<CategoryModel>))]
        public ICategoryModel Category { get; set; }
    }
}