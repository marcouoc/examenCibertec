using Xunit;
using FluentAssertions;

using System;
using Moq;
using System.Data.Entity;
using System.Linq;
using WebExamenDoFactory.Repositorio;

namespace UnitTest.Repositorio
{
    public class BaseRepositorioTest
    {
       private WebContextDb dbContext;

        public BaseRepositorioTest()
        {
            dbContext = new WebContextDb();
        }

        public void BaseRepositorioBDTest()
        {
            dbContext.Customer.Should().NotBeNull();
            dbContext.Product.Should().NotBeNull();
            dbContext.Order.Should().NotBeNull();
            dbContext.OrderItem.Should().NotBeNull();
            dbContext.Supplier.Should().NotBeNull();

        }
    }
}
