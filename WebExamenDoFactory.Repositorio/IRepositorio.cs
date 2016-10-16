using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace WebExamenDoFactory.Repositorio
{
    public interface  IRepositorio<T>
    {
       
        int Insertar(T entidad);
        int Actualizar(T entidad);
        int Eliminar(T entidad);
        List<T> ObtenerLista();
        T ObtenerPorId(Expression<Func<T, bool>> match);
        IEnumerable<T> OrderedListByDateAndSize(Expression<Func<T, DateTime>> match, int size);
        IEnumerable<T> PaginacionLista(Expression<Func<T, DateTime>> match, int page, int size);

        IEnumerable<T> PaginacionListaPorApellido(Expression<Func<T, string>> match, int id, int size);


        IEnumerable<T> ListaPorId(Expression<Func<T, bool>> match);

    }
}
