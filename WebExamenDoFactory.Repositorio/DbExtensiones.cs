using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebExamenDoFactory.Repositorio
{
    public static class DbExtensiones
    {
        public static IEnumerable<TSource> Pagina<TSource>(
            this IEnumerable<TSource> source,
            int page,
            int size)
        {
            return source.Skip((page - 1) * size).Take(size);
        }
    }
}
