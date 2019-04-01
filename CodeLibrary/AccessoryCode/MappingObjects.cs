using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;

namespace CodeLibrary.AccessoryCode
{
    public static class MappingObjects
    {
        public static ISoldProductModel ProductToSoldProduct(IProductModel product, int idTable, IDataAccessSubCategory<ITableModel> tableData)
        {
            ISoldProductModel soldProduct = Factory.InstanceSoldProductModel();

            soldProduct.Name = product.Name;
            soldProduct.Price = product.Price;
            soldProduct.CategoryID = product.CategoryID;
            soldProduct.TableID = idTable;
            soldProduct.Detail = "";
            soldProduct.Category.ID = product.CategoryID;
            soldProduct.Category.Name = product.Category.Name;
            soldProduct.Table = tableData.FindById(idTable);

            return soldProduct;
        }

        public static ISoldProductAccomplishedModel SoldProductToSoldProductAccomplished(ISoldProductModel product)
        {
            ISoldProductAccomplishedModel soldProductAccomplished = Factory.InstanceSoldProductAccomplishedModel();

            soldProductAccomplished.Name = product.Name;
            soldProductAccomplished.CategoryID = product.CategoryID;
            soldProductAccomplished.Price = product.Price;
            soldProductAccomplished.Category.ID = product.CategoryID;
            soldProductAccomplished.Category.Name = product.Category.Name;

            return soldProductAccomplished;
        }
    }
}