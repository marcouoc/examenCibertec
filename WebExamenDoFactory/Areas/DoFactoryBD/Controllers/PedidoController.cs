using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebExamenDoFactory.Model;
using WebExamenDoFactory.Repositorio;

namespace WebExamenDoFactory.Areas.DoFactoryBD.Controllers
{
    public class PedidoController : FactoryBaseController<Order>
    {
        // GET: DoFactoryBD/Producto

        public PedidoController(IRepositorio<Order> repositorio) : base(repositorio)
        {
        }

        public ActionResult Index()
        {
            return View();
            //return View(_repositorio.OrderedListByDateAndSize());

        }
        public ActionResult Create()
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
            return PartialView("_List", _repositorio.PaginacionListaPorApellido((x => x.Id.ToString()),
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
        public ActionResult Create(Order order)
        {
            if (!ModelState.IsValid) return View(order);


            //customer.CustomerID = Guid.NewGuid();
            _repositorio.Insertar(order);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(_repositorio.ObtenerPorId(x => x.Id == id));
        }

        public ActionResult Details(int id)
        {
            return View(_repositorio.ObtenerPorId(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (!ModelState.IsValid) return View(order);


            _repositorio.Actualizar(order);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(_repositorio.ObtenerPorId(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");

            var producto = _repositorio.ObtenerPorId(x => x.Id == id);
            if (producto == null) return RedirectToAction("Index");

            _repositorio.Eliminar(producto);
            return RedirectToAction("Index");
        }
    }
}
