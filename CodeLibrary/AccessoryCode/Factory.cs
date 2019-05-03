using CodeLibrary.EntityFramework;
using System.Collections.Generic;
using CodeLibrary.ModelsMVC;
using CodeLibrary.DataAccess;
using CodeLibrary.DataAccess.WebAPIAccess;

namespace CodeLibrary.AccessoryCode
{
    public static class Factory
    {
        public static RestaurantContext InstanceRestaurantContext()
        {
            return new RestaurantContext();
        }

        public static IDataAccessRegular<IAreaModel> InstanceAreaDataAccess()
        {
            if (TypeOfAccess.Access == Connection.WebApi)
            {
                return new AreaDataAccessWebAPI();
            }
            else
            {
                return new AreaDataAccess(InstanceRestaurantContext());
            }               
        }

        public static IDataAccessRegular<ICategoryModel> InstanceCategoryDataAccess()
        {
            if (TypeOfAccess.Access == Connection.WebApi)
            {
                return new CategoryDataAccessWebAPI();
            }
            else
            {
                return new CategoryDataAccess(InstanceRestaurantContext());
            }
        }

        public static IDataAccessSubCategory<ITableModel> InstanceTableDataAccess()
        {
            if (TypeOfAccess.Access == Connection.WebApi)
            {
                return new TableDataAccessWebAPI();
            }
            else
            {
                return new TableDataAccess(InstanceRestaurantContext());
            }
        }

        public static IDataAccessSubCategory<IProductModel> InstanceProductDataAccess()
        {
            if (TypeOfAccess.Access == Connection.WebApi)
            {
                return new ProductDataAccessWebAPI();
            }
            else
            {
                return new ProductDataAccess(InstanceRestaurantContext());
            }
        }

        public static ISoldProductDataAccess InstanceSoldProductDataAccess()
        {
            if (TypeOfAccess.Access == Connection.WebApi)
            {
                return new SoldProductDataAccessWebAPI();
            }
            else
            {
                return new SoldProductDataAccess(InstanceRestaurantContext());
            }
        }

        public static ISoldProductAccomplishedDataAccess InstanceSoldProductAccomplishedDataAccess()
        {
            if (TypeOfAccess.Access == Connection.WebApi)
            {
                return new SoldProductAccomplishedDataAccessWebAPI();
            }
            else
            {
                return new SoldProductAccomplishedDataAccess(InstanceRestaurantContext());
            }
        }

        public static ISaleDataAccess InstanceSaleDataAccess()
        {
            if (TypeOfAccess.Access == Connection.WebApi)
            {
                return new SaleDataAccessWebAPI();
            }
            else
            {
                return new SaleDataAccess(InstanceRestaurantContext());
            }
        }

        public static List<ISaleModel> InstanceISaleModelList()
        {
            return new List<ISaleModel>();
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

        public static ISaleModel InstanceSaleModel()
        {
            return new SaleModel();
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