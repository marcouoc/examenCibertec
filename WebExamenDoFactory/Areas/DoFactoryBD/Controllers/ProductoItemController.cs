using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebExamenDoFactory.Model;

namespace WebExamenDoFactory.Areas.DoFactoryBD.Controllers
{
    public class ProductoItemController : FactoryBaseController<OrderItem>
    {
        public ActionResult Index()
        {
            //return View();
            return View(_repositorio.PaginacionListaPorApellido((x => x.Product.ProductName), 1, 30));

        }
        public ActionResult Creacion()
        {
            return View();
        }
        public ActionResult List(int? page, int? size)
        {
            if (!page.HasValue || !size.HasValue)
            {
                page = 1;
                size = 15;
            }
            return PartialView("_List", _repositorio.PaginacionListaPorApellido((x => x.Product.ProductName),
                page.Value,
                size.Value));
        }

        public int PaginaTotal(int rows)
        {
            if (rows <= 0) return 0;
            var count = _repositorio.ObtenerLista().Count;
            return count % rows > 0 ? (count / rows) + 1 : count / rows;
        }

        [HttpPost]
        public ActionResult Creacion(OrderItem orderItem)
        {
            if (!ModelState.IsValid) return View(orderItem);


            //customer.CustomerID = Guid.NewGuid();
            _repositorio.Insertar(orderItem);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            return View(_repositorio.ObtenerPorId(x => x.Id == id));
        }

        public ActionResult Detalles(int id)
        {
            return View(_repositorio.ObtenerPorId(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Editar(OrderItem orderItem)
        {
            if (!ModelState.IsValid) return View(orderItem);


            _repositorio.Actualizar(orderItem);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            return View(_repositorio.ObtenerPorId(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Eliminar(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");

            var producto = _repositorio.ObtenerPorId(x => x.Id == id);
            if (producto == null) return RedirectToAction("Index");

            _repositorio.Eliminar(producto);
            return RedirectToAction("Index");
        }

    }
}