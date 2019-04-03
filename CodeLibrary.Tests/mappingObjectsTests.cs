using CodeLibrary.AccessoryCode;
using CodeLibrary.ModelsMVC;
using Xunit;
using FluentAssertions;

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

            Assert.Equal(soldProductAccomplished.Name, soldProduct.Name);
            Assert.Equal(soldProductAccomplished.Price, soldProduct.Price);
            soldProductAccomplished.Category.Should().BeEquivalentTo(soldProduct.Category);
        }
    }
}
