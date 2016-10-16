using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebExamenDoFactory.Model;
using WebExamenDoFactory.Repositorio;
using WebExamenDoFactory.Areas.DoFactoryBD;
using WebExamenDoFactory.Areas.DoFactoryBD.Controllers;

using Xunit;

namespace UnitTest.Controladores
{
    public class ClienteControllerTest
 {
          private ClienteController controller;
        private IRepositorio<Customer> _repositorio;
        private Mock<DbSet<Customer>> clienteDbSetMock;
        private Mock<WebContextDb> webContextMock;


        [Fact(DisplayName = "ListaVacioParametrosTest")]
        private void ListaVacioParametrosTest()
        {
            ListConfigMockData();
            controller = new ClienteController(_repositorio);
            var result = controller.List(null, null) as PartialViewResult;
            result.ViewName.Should().Be("_List");

            var modelCount = (IEnumerable<Customer>)result.Model;
            modelCount.Count().Should().Be(10);
        }

        [Fact(DisplayName = "CrearTest")]
        private void CrearTest()
        {
           BasicConfigMockData();
            controller = new ClienteController(_repositorio);
            var result = controller.Create() as PartialViewResult;
            result.ViewName.Should().Be("_Create");

            var personModelCreate = (Customer)result.Model;
            personModelCreate.Should().NotBeNull();
        }

        [Fact(DisplayName = "CrearPostTestOk")]
        private void CrearPostTestOk()
        {
            BasicConfigMockData();
            controller = new ClienteController(_repositorio);
            var result = controller.Create(TestClientOK()) as PartialViewResult;
            result.Should().BeNull();

            clienteDbSetMock.Verify(s => s.Add(It.IsAny<Customer>()), Times.Once());
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact(DisplayName = "CreatePostTestWrong")]
        private void CreatePostTestWrong()
        {
            BasicConfigMockData();
            controller = new ClienteController(_repositorio);
            var personToFail = TestClientIncorrecto();
            controller.ModelState.AddModelError("errorTest", "errorTest");
            var result = controller.Create(personToFail) as PartialViewResult;
            result.ViewName.Should().Be("_Create");

            var personModelCreate = (Customer)result.Model;
            personModelCreate.Should().Be(personToFail);

        }

        private Customer TestClientIncorrecto()
        {
            var c = new Customer
            {
                FirstName = "Wrong",
                LastName = "Wrong"
            };
            return c;
        }



        private Customer TestClientOK()
        {
            var c = new Customer
            {
                FirstName = "test",
                City = "city",
                Country = "country",
                LastName = "apellido",
                Phone = "1234444"

            };
            return c;
        }



        public void ClienteMockList()
        {
            var persons = Enumerable.Range(1, 10).Select(i => new Customer
            {
                Country = "Country",
                FirstName = $"Name{i}",
                LastName = $"LastName{i}",
                Phone ="12222",
                City="City"
            }).AsQueryable();
            clienteDbSetMock = new Mock<DbSet<Customer>>();
            clienteDbSetMock.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(persons.Provider);
            clienteDbSetMock.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(persons.Expression);
            clienteDbSetMock.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            clienteDbSetMock.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());
        }

        private void ListConfigMockData()
        {
            clienteDbSetMock = new Mock<DbSet<Customer>>();
            ClienteMockList();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Customer).Returns(clienteDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Customer>()).Returns(clienteDbSetMock.Object);

            _repositorio = new BaseRepositorio<Customer>(webContextMock.Object);
            controller = new ClienteController(_repositorio);
        }

        private void BasicConfigMockData()
        {
           clienteDbSetMock = new Mock<DbSet<Customer>>();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Customer).Returns(clienteDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Customer>()).Returns(clienteDbSetMock.Object);

            _repositorio = new BaseRepositorio<Customer>(webContextMock.Object);
            controller = new ClienteController(_repositorio);
        }
    }

}
