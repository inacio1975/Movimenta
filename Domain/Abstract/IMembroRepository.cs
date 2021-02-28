using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IMembroRepository
    {
        void AddMembro(Membro membro);
        bool EliminaMembro(int id);

        List<Membro> GetMembros();
        List<Membro> GetMembros(string selector);
        Membro GetMembro(int id);
    }
}
