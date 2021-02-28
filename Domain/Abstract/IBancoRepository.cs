using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IBancoRepository
    {
        void AddBanco(Banco banco);
        bool EliminBanco(int id);

        List<Banco> GetBancos();
        List<Banco> GetBancos(string selector);
        Banco GetBancos(int id);
    }
}
