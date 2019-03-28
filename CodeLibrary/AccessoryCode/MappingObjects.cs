using CodeLibrary.ModelsMVC;

namespace CodeLibrary.AccessoryCode
{
    public static class MappingObjects
    {
        public static ISoldProductModel ProductToSoldProduct(IProductModel product, int idTable)
        {
            ISoldProductModel soldProduct = Factory.InstanceSoldProductModel();

            soldProduct.Name = product.Name;
            soldProduct.Price = product.Price;
            soldProduct.CategoryID = product.CategoryID;
            soldProduct.TableID = idTable;
            soldProduct.Detail = "";
            soldProduct.Category.ID = product.CategoryID;
            soldProduct.Category.Name = product.Category.Name;

            return soldProduct;
        }

        public static ISoldProductAccomplishedModel SoldProductToSoldProductAccomplished(ISoldProductModel product)
        {
            ISoldProductAccomplishedModel soldProductAccomplished = Factory.InstanceSoldProductAccomplishedModel();

            soldProductAccomplished.Name = product.Name;
            soldProductAccomplished.CategoryID = product.CategoryID;
            soldProductAccomplished.Price = product.Price;

            return soldProductAccomplished;
        }
    }
}