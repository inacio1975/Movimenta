using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class MembroUsuarioRepository : IMembroUsuarioRepository
    {
        public void AddElement(MembroUsuario elemento)
        {
            throw new NotImplementedException();
        }

        public bool EliminaElement(int id)
        {
            throw new NotImplementedException();
        }

        public List<Membro> GetMembros()
        {
            throw new NotImplementedException();
        }

        public List<Membro> GetMembros(string selector)
        {
            throw new NotImplementedException();
        }

        public Membro GetMembro(int id)
        {
            throw new NotImplementedException();
        }
    }
}
