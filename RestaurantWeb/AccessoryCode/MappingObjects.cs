using RestaurantWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWeb.AccessoryCode
{
    public static class MappingObjects
    {
        public static ISoldProductModel ProductToSoldProduct(IProductModel product, int idTable)
        {
            ISoldProductModel soldProduct = ObjectCreator.SoldProductModel();

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
            ISoldProductAccomplishedModel soldProductAccomplished = ObjectCreator.SoldProductAccomplishedModel();

            soldProductAccomplished.Name = product.Name;
            soldProductAccomplished.CategoryID = product.CategoryID;
            soldProductAccomplished.Price = product.Price;

            return soldProductAccomplished;
        }
    }
}