using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebExamenDoFactory.Areas.DoFactoryBD.Controllers;
using WebExamenDoFactory.Model;
using WebExamenDoFactory.Repositorio;
using Xunit;

namespace UnitTest.Controladores
{
    public class ProductoControllerTest
    {
        private ProductoController controller;
        private IRepositorio<Product> _repositorio;
        private Mock<DbSet<Product>> DbSetMock;
        private Mock<WebContextDb> webContextMock;


        [Fact(DisplayName = "ListaVacioParametrosTest")]
        private void ListaVacioParametrosTest()
        {
            ListConfigMockData();
            controller = new ProductoController(_repositorio);
            var result = controller.List(null, null) as PartialViewResult;
            result.ViewName.Should().Be("_List");

            var modelCount = (IEnumerable<Product>)result.Model;
            modelCount.Count().Should().Be(10);
        }

        [Fact(DisplayName = "CrearTest")]
        private void CrearTest()
        {
            BasicConfigMockData();
            controller = new ProductoController(_repositorio);
            var result = controller.Create() as PartialViewResult;
            result.ViewName.Should().Be("_Create");

            var personModelCreate = (Product)result.Model;
            personModelCreate.Should().NotBeNull();
        }

        [Fact(DisplayName = "CrearPostTestOk")]
        private void CrearPostTestOk()
        {
            BasicConfigMockData();
            controller = new ProductoController(_repositorio);
            var result = controller.Create(TestClientOK()) as PartialViewResult;
            result.Should().BeNull();

            DbSetMock.Verify(s => s.Add(It.IsAny<Product>()), Times.Once());
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact(DisplayName = "CreatePostTestWrong")]



        private Product TestClientIncorrecto()
        {
            var c = new Product
            {
                ProductName ="error",
                Package="error"
                
            };
            return c;
        }



        private Product TestClientOK()
        {
            var c = new Product
            {
                ProductName = "p",
                Package = "ss",
                UnitPrice = 1,
                IsDiscontinued = true

            };
            return c;
        }



        public void ProductMockList()
        {
            var persons = Enumerable.Range(1, 10).Select(i => new Product
            {
               ProductName="p",
               Package="ss",
               UnitPrice=1,
               IsDiscontinued=true,
            }).AsQueryable();
            DbSetMock = new Mock<DbSet<Product>>();
            DbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(persons.Provider);
            DbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(persons.Expression);
            DbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            DbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());
        }

        private void ListConfigMockData()
        {
            DbSetMock = new Mock<DbSet<Product>>();
            ProductMockList();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Product).Returns(DbSetMock.Object);
            webContextMock.Setup(m => m.Set<Product>()).Returns(DbSetMock.Object);

            _repositorio = new BaseRepositorio<Product>(webContextMock.Object);
            controller = new ProductoController(_repositorio);
        }

        private void BasicConfigMockData()
        {
            DbSetMock = new Mock<DbSet<Product>>();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Product).Returns(DbSetMock.Object);
            webContextMock.Setup(m => m.Set<Product>()).Returns(DbSetMock.Object);

            _repositorio = new BaseRepositorio<Product>(webContextMock.Object);
            controller = new ProductoController(_repositorio);
        }

    }
}
