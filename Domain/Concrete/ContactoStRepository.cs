using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class ContactoStRepository : IContactoStRepository
    {
        public void AddContacto(ContactoSt contacto)
        {
            throw new NotImplementedException();
        }

        public bool EliminContacto(int id)
        {
            throw new NotImplementedException();
        }

        public List<ContactoSt> GetContactos()
        {
            throw new NotImplementedException();
        }

        public List<ContactoSt> GetContactos(string selector)
        {
            throw new NotImplementedException();
        }

        public ContactoSt GetContactos(int id)
        {
            throw new NotImplementedException();
        }
    }
}
