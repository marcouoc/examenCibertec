using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebExamenDoFactory.Areas.DoFactoryBD.Controllers;
using WebExamenDoFactory.Model;
using WebExamenDoFactory.Repositorio;

namespace UnitTest.Controladores
{
    public class ProveedorControllerTest
    {
        private ProveedorController controller;
        private IRepositorio<Supplier> _repositorio;
        private Mock<DbSet<Supplier>> DbSetMock;
        private Mock<WebContextDb> webContextMock;
    }
}
