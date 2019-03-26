﻿using RestaurantWeb.InternalServices;
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

        public static IDataAccessRegular<IAreaModel> InstanceAreaDataAccess()
        {
            return new AreaDataAccess(InstanceRestaurantContext());
        }

        public static IDataAccessRegular<ICategoryModel> InstanceCategoryDataAccess()
        {
            return new CategoryDataAccess(InstanceRestaurantContext());
        }

        public static IDataAccessSubCategory<ITableModel> InstanceTableDataAccess()
        {
            return new TableDataAccess(InstanceRestaurantContext());
        }

        public static IDataAccessSubCategory<IProductModel> InstanceProductDataAccess()
        {
            return new ProductDataAccess(InstanceRestaurantContext());
        }

        public static ISoldProductDataAccess InstanceSoldProductDataAccess()
        {
            return new SoldProductDataAccess(InstanceRestaurantContext());
        }

        public static ISoldProductAccomplishedDataAccess InstanceSoldProductAccomplishedDataAccess()
        {
            return new SoldProductAccomplishedDataAccess(InstanceRestaurantContext());
        }

        public static ISalleDataAccess InstanceSalleDataAccess()
        {
            return new SalleDataAccess(InstanceRestaurantContext());
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

        public static IMainPageModel InstanceMainPageModel()
        {
            return new MainPageModel(InstanceIAreaModelList(), InstanceITableModelList(), InstanceICategoryModelList(), InstanceIProductModelList());
        }

        public static ITableModel InstanceTableModel()
        {
            return new TableModel(InstanceAreaModel(), InstanceISoldProductModelList());
        }

        public static IProductModel InstanceProductModel()
        {
            return new ProductModel(InstanceCategoryModel());
        }

        public static IAreaModel InstanceAreaModel()
        {
            return new AreaModel();
        }

        public static ICategoryModel InstanceCategoryModel()
        {
            return new CategoryModel();
        }

        public static ISalleModel InstanceSalleModel()
        {
            return new SalleModel();
        }

        public static ISoldProductModel InstanceSoldProductModel()
        {
            return new SoldProductModel(InstanceCategoryModel(), InstanceTableModel());
        }

        public static ISoldProductAccomplishedModel InstanceSoldProductAccomplishedModel()
        {
            return new SoldProductAccomplishedModel(InstanceCategoryModel());
        }
    }
}