using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IContactoStRepository
    {
        void AddContacto(ContactoSt contacto);
        bool EliminContacto(int id);

        List<ContactoSt> GetContactos();
        List<ContactoSt> GetContactos(string selector);
        ContactoSt GetContactos(int id);
    }
}
