using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IContactoRepository
    {
        void AddContacto(Contacto contacto);
        bool EliminContacto(int id);

        List<Contacto> GetContactos();
        List<Contacto> GetContactos(string selector);
        Contacto GetContactos(int id);
    }
}
