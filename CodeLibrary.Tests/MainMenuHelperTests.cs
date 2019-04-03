using CodeLibrary.AccessoryCode;
using CodeLibrary.DataAccess;
using CodeLibrary.ModelsMVC;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CodeLibrary.Tests
{
    public class MainMenuHelperTests
    {
        [Fact]
        public void PaySelectedSoldProducts_ShouldWork()
        {
            List<ISoldProductModel> inputList = Factory.InstanceISoldProductModelList();
            ISoldProductModel product1 = Factory.InstanceSoldProductModel();
            ISoldProductModel product2 = Factory.InstanceSoldProductModel();

            product1.ID = 23;
            product1.Name = "product1";
            product1.Price = 1.5M;
            product1.Detail = "";

            product2.ID = 12;
            product2.Name = "product2";
            product2.Price = 3.5M;
            product2.Detail = "algo";           

            inputList.Add(product1);
            inputList.Add(product2);

            int[] elements = { 0 };           

            Mock<ISoldProductDataAccess> soldproductData = new Mock<ISoldProductDataAccess>();
            Mock<ISoldProductAccomplishedDataAccess> soldproductDataAccomplished = new Mock<ISoldProductAccomplishedDataAccess>();

            MainMenuHelper.PaySelectedSoldProducts(inputList, elements, soldproductData.Object, soldproductDataAccomplished.Object);

            Assert.NotEmpty(inputList);
            Assert.Single(inputList);
            inputList[0].Should().BeEquivalentTo(product2);
        }

        [Fact]
        public void PaySelectedSoldProducts_ShouldGiveException()
        {
            List<ISoldProductModel> inputList = Factory.InstanceISoldProductModelList();
            ISoldProductModel product1 = Factory.InstanceSoldProductModel();
            ISoldProductModel product2 = Factory.InstanceSoldProductModel();

            product1.ID = 23;
            product1.Name = "product1";
            product1.Price = 1.5M;
            product1.Detail = "";

            product2.ID = 12;
            product2.Name = "product2";
            product2.Price = 3.5M;
            product2.Detail = "algo";

            inputList.Add(product1);
            inputList.Add(product2);

            int[] elements = { 0, 3 };

            Mock<ISoldProductDataAccess> soldproductData = new Mock<ISoldProductDataAccess>();
            Mock<ISoldProductAccomplishedDataAccess> soldproductDataAccomplished = new Mock<ISoldProductAccomplishedDataAccess>();

            Action act = () => MainMenuHelper.PaySelectedSoldProducts(inputList, elements, soldproductData.Object, soldproductDataAccomplished.Object);

            Assert.Throws<ArgumentOutOfRangeException>(act);
        }
    }
}
