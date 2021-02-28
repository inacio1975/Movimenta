using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Concrete
{
    public class MunicipioRepository
    {
        public List<Municipio> GetMunicipioProvincia(int id)
        {
            using (var context = new MovimentaContext())
            {
                //return (from provincia in context.Provincias where provincia.IdPais == id select provincia).ToList();
                return context.Municipios.Where(municipio => municipio.IdProvincia == id).ToList();
            }
        }
    }
}
