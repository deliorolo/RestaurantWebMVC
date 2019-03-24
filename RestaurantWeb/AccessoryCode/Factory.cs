using RestaurantWeb.InternalServices;
using RestaurantWeb.Models;
using RestaurantWeb.Models.EF;
using System.Collections.Generic;

namespace RestaurantWeb.AccessoryCode
{
    public static class Factory
    {
        public static RestaurantContext InstanceRestaurantContext()
        {
            return new RestaurantContext();
        }

        public static AreaDataAccess InstanceAreaDataAccess()
        {
            return new AreaDataAccess();
        }

        public static CategoryDataAccess InstanceCategoryDataAccess()
        {
            return new CategoryDataAccess();
        }

        public static TableDataAccess InstanceTableDataAccess()
        {
            return new TableDataAccess();
        }

        public static ProductDataAccess InstanceProductDataAccess()
        {
            return new ProductDataAccess();
        }

        public static SoldProductDataAccess InstanceSoldProductDataAccess()
        {
            return new SoldProductDataAccess();
        }

        public static SoldProductAccomplishedDataAccess InstanceSoldProductAccomplishedDataAccess()
        {
            return new SoldProductAccomplishedDataAccess();
        }

        public static SalleDataAccess InstanceSalleDataAccess()
        {
            return new SalleDataAccess();
        }

        public static List<ISalleModel> InstanceISalleModelList()
        {
            return new List<ISalleModel>();
        }

        public static List<ITableModel> InstanceITableModelList()
        {
            return new List<ITableModel>();
        }

        public static List<IProductModel> InstanceIProductModelList()
        {
            return new List<IProductModel>();
        }

        public static List<IAreaModel> InstanceIAreaModelList()
        {
            return new List<IAreaModel>();
        }

        public static List<ICategoryModel> InstanceICategoryModelList()
        {
            return new List<ICategoryModel>();
        }

        public static List<ISoldProductModel> InstanceISoldProductModelList()
        {
            return new List<ISoldProductModel>();
        }

        public static List<ISoldProductAccomplishedModel> InstanceISoldProductAccomplishedModelList()
        {
            return new List<ISoldProductAccomplishedModel>();
        }

        public static MainPageModel InstanceMainPageModel()
        {
            return new MainPageModel();
        }

        public static TableModel InstanceTableModel()
        {
            return new TableModel();
        }

        public static ProductModel InstanceProductModel()
        {
            return new ProductModel();
        }

        public static AreaModel InstanceAreaModel()
        {
            return new AreaModel();
        }

        public static CategoryModel InstanceCategoryModel()
        {
            return new CategoryModel();
        }

        public static SalleModel InstanceSalleModel()
        {
            return new SalleModel();
        }

        public static SoldProductModel InstanceSoldProductModel()
        {
            return new SoldProductModel();
        }

        public static SoldProductAccomplishedModel InstanceSoldProductAccomplishedModel()
        {
            return new SoldProductAccomplishedModel();
        }
    }
}