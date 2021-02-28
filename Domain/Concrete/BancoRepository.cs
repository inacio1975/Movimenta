using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class BancoRepository : IBancoRepository
    {
        public void AddBanco(Banco banco)
        {
            throw new NotImplementedException();
        }

        public bool EliminBanco(int id)
        {
            throw new NotImplementedException();
        }

        public List<Banco> GetBancos()
        {
            throw new NotImplementedException();
        }

        public List<Banco> GetBancos(string selector)
        {
            throw new NotImplementedException();
        }

        public Banco GetBancos(int id)
        {
            throw new NotImplementedException();
        }
    }
}
