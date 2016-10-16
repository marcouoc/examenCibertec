using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebExamenDoFactory.Repositorio;

namespace WebExamenDoFactory.Areas.DoFactoryBD.Controllers
{
    public class FactoryBaseController <T> : Controller where T : class
    {
        protected IRepositorio<T> _repositorio;
        public FactoryBaseController()
        {
            _repositorio = new BaseRepositorio<T>();
        }
    }
}