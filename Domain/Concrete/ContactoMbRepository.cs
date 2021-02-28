using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class ContactoMbRepository : IContactoMbRepository
    {
        public void AddContacto(ContactoMb contacto)
        {
            throw new NotImplementedException();
        }

        public bool EliminContacto(int id)
        {
            throw new NotImplementedException();
        }

        public List<ContactoMb> GetContactos()
        {
            throw new NotImplementedException();
        }

        public List<ContactoMb> GetContactos(string selector)
        {
            throw new NotImplementedException();
        }

        public ContactoMb GetContactos(int id)
        {
            throw new NotImplementedException();
        }
    }
}
