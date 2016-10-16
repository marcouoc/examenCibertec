using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebExamenDoFactory.Repositorio
{
    public class BaseRepositorio<T> : IRepositorio<T> where T : class
    {

        protected WebContextDb db;
        public BaseRepositorio()
        {
            db = new WebContextDb();
        }

        public int Actualizar(T entidad)
        {
            db.Entry(entidad).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Eliminar(T entidad)
        {
            db.Entry(entidad).State = EntityState.Deleted;
            return db.SaveChanges();
        }

        public int Insertar(T entidad)
        {
            db.Entry(entidad).State = EntityState.Added;
            return db.SaveChanges();
        }

        public IEnumerable<T> ListaPorId(Expression<Func<T, bool>> match)
        {
            throw new NotImplementedException();
        }

        public List<T> ObtenerLista()
        {
            return db.Set<T>().ToList();
        }

        public T ObtenerPorId(Expression<Func<T, bool>> match)
        {
            return db.Set<T>().FirstOrDefault(match);
        }

        public IEnumerable<T> OrderedListByDateAndSize(Expression<Func<T, DateTime>> match, int size)
        {
            return db.Set<T>().OrderByDescending(match).Take(size);
        }

        public IEnumerable<T> PaginacionLista(Expression<Func<T, DateTime>> match, int page, int size)
        {
            return db.Set<T>().OrderByDescending(match).Pagina(page, size);
        }

        public IEnumerable<T> PaginacionListaPorApellido(Expression<Func<T, string>> match, int id, int size)
        {
            return db.Set<T>().OrderByDescending(match).Pagina(id, size);
        }
    }
}
