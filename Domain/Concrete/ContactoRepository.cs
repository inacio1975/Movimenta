using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class ContactoRepository : IContactoRepository
    {
        public void AddContacto(Contacto contacto)
        {
            throw new NotImplementedException();
        }

        public bool EliminContacto(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contacto> GetContactos()
        {
            throw new NotImplementedException();
        }

        public List<Contacto> GetContactos(string selector)
        {
            throw new NotImplementedException();
        }

        public Contacto GetContactos(int id)
        {
            throw new NotImplementedException();
        }
    }
}
