using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebExamenDoFactory.Model;

namespace WebExamenDoFactory.Areas.DoFactoryBD.Controllers
{
    public class ProveedorController : FactoryBaseController<Supplier>
    {
      
        public ActionResult Index()
        {
            //return View();
            return View(_repositorio.PaginacionListaPorApellido((x => x.CompanyName), 1, 30));

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
            return PartialView("_List", _repositorio.PaginacionListaPorApellido((x => x.CompanyName),
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
        public ActionResult Creacion(Supplier proveedor)
        {
            if (!ModelState.IsValid) return View(proveedor);


            //customer.CustomerID = Guid.NewGuid();
            _repositorio.Insertar(proveedor);
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
        public ActionResult Editar(Supplier proveedor)
        {
            if (!ModelState.IsValid) return View(proveedor);


            _repositorio.Actualizar(proveedor);
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