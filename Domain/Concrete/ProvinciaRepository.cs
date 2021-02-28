using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class ProvinciaRepository
    {
        public List<Provincia> GetProvinvias()
        {
            using (var context = new MovimentaContext())
            {
                return context.Provincias.ToList();
            }
        }

        public List<Provincia> GetProvinciasPais(int id)
        {
            using (var context = new MovimentaContext())
            {
                //return (from provincia in context.Provincias where provincia.IdPais == id select provincia).ToList();
                return context.Provincias.Where(provincia => provincia.IdPais == id).ToList();
            }
        }

        public Provincia GetProvincia(int id)
        {
            using (var context = new MovimentaContext())
            {
                return (from fila in context.Provincias
                        where fila.IdProvincia == id
                        select fila).SingleOrDefault();
            }
        }
    }
}
