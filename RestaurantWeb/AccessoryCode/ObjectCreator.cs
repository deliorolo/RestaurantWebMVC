using RestaurantWeb.Models;
using RestaurantWeb.InternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantWeb.Models.EF;

namespace RestaurantWeb.AccessoryCode
{
    public static class ObjectCreator
    {
        public static RestaurantContext RestaurantContext()
        {
            return new RestaurantContext();
        }

        public static AreaDataAccess AreaDataAccess()
        {
            return new AreaDataAccess();
        }

        public static CategoryDataAccess CategoryDataAccess()
        {
            return new CategoryDataAccess();
        }

        public static TableDataAccess TableDataAccess()
        {
            return new TableDataAccess();
        }

        public static ProductDataAccess ProductDataAccess()
        {
            return new ProductDataAccess();
        }

        public static SoldProductDataAccess SoldProductDataAccess()
        {
            return new SoldProductDataAccess();
        }

        public static SoldProductAccomplishedDataAccess SoldProductAccomplishedDataAccess()
        {
            return new SoldProductAccomplishedDataAccess();
        }

        public static SalleDataAccess SalleDataAccess()
        {
            return new SalleDataAccess();
        }

        public static List<ISalleModel> ISalleModelList()
        {
            return new List<ISalleModel>();
        }

        public static List<ITableModel> ITableModelList()
        {
            return new List<ITableModel>();
        }

        public static List<IProductModel> IProductModelList()
        {
            return new List<IProductModel>();
        }

        public static List<IAreaModel> IAreaModelList()
        {
            return new List<IAreaModel>();
        }

        public static List<ICategoryModel> ICategoryModelList()
        {
            return new List<ICategoryModel>();
        }

        public static List<ISoldProductModel> ISoldProductModelList()
        {
            return new List<ISoldProductModel>();
        }

        public static List<ISoldProductAccomplishedModel> ISoldProductAccomplishedModelList()
        {
            return new List<ISoldProductAccomplishedModel>();
        }

        public static MainPageModel MainPageModel()
        {
            return new MainPageModel();
        }

        public static TableModel TableModel()
        {
            return new TableModel();
        }

        public static ProductModel ProductModel()
        {
            return new ProductModel();
        }

        public static AreaModel AreaModel()
        {
            return new AreaModel();
        }

        public static CategoryModel CategoryModel()
        {
            return new CategoryModel();
        }

        public static SalleModel SalleModel()
        {
            return new SalleModel();
        }

        public static SoldProductModel SoldProductModel()
        {
            return new SoldProductModel();
        }

        public static SoldProductAccomplishedModel SoldProductAccomplishedModel()
        {
            return new SoldProductAccomplishedModel();
        }
    }
}