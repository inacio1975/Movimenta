using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class PaisRepository
    {
        public List<Pais> GetPaises()
        {
            using (var context = new MovimentaContext())
            {
                return context.Paises.ToList();
            }
        }

        public Pais GetPais(int id)
        {
            using (var context = new MovimentaContext())
            {
                return (from fila in context.Paises
                        where fila.IdPais == id
                        select fila).SingleOrDefault();
            }
        }
    }
}
