using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IContactoMbRepository
    {
        void AddContacto(ContactoMb contacto);
        bool EliminContacto(int id);

        List<ContactoMb> GetContactos();
        List<ContactoMb> GetContactos(string selector);
        ContactoMb GetContactos(int id);
    }
}
