using CodeLibrary.AccessoryCode;
using CodeLibrary.ModelsMVC;
using Xunit;
using FluentAssertions;
using Moq;
using CodeLibrary.DataAccess;

namespace CodeLibrary.Tests
{
    public class MappingObjectsTests
    {
        [Fact]
        public void SoldProductToSoldProductAccomplished_ShouldBeMapped()
        {
            ISoldProductModel soldProduct = Factory.InstanceSoldProductModel();
            soldProduct.Name = "some product";
            soldProduct.Price = 999.93M;
            soldProduct.Category.Name = "some category";
            soldProduct.Category.ID = 897;
            soldProduct.CategoryID = 897;

            ISoldProductAccomplishedModel soldProductAccomplished = MappingObjects.SoldProductToSoldProductAccomplished(soldProduct);

            Assert.Equal(soldProduct.Name, soldProductAccomplished.Name);
            Assert.Equal(soldProduct.Price, soldProductAccomplished.Price);
            soldProductAccomplished.Category.Should().BeEquivalentTo(soldProduct.Category);
        }

        [Fact]
        public void ProductToSoldProduct_ShouldBeMapped()
        {
            IProductModel product = Factory.InstanceProductModel();           

            product.Name = "some product";
            product.Price = 888.45M;
            product.CategoryID = 654;
            product.Category.ID = 654;
            product.Category.Name = "some category";

            int idTable = 36;

            Mock<IDataAccessSubCategory<ITableModel>> data = new Mock<IDataAccessSubCategory<ITableModel>>();
            data.Setup(x => x.FindById(idTable)).
                Returns(new TableModel() { ID = 36, AreaID = 7, NumberOfTable = 9765, Occupied = true, Area = new AreaModel { ID = 7, Name = "some area" } });

            ISoldProductModel soldProduct = MappingObjects.ProductToSoldProduct(product, idTable, data.Object);

            Assert.Equal(product.Name, soldProduct.Name);
            Assert.Equal(product.Price, soldProduct.Price);
            Assert.Equal("", soldProduct.Detail);
            soldProduct.Category.Should().BeEquivalentTo(product.Category);
            soldProduct.Table.Should().BeEquivalentTo(data.Object.FindById(idTable));
        }
    }
}
