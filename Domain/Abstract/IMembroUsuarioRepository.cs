using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IMembroUsuarioRepository
    {
        void AddElement(MembroUsuario elemento);
        bool EliminaElement(int id);

        List<Membro> GetMembros();
        List<Membro> GetMembros(string selector);
        Membro GetMembro(int id);

    }
}
