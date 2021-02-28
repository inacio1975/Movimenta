using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EnderecoStRepository : IEnderecoStRepository
    {
        public void AddEndereco(EnderecoSt enderecoMb)
        {
            throw new NotImplementedException();
        }

        public bool EliminEndereco(int id)
        {
            throw new NotImplementedException();
        }

        public List<EnderecoSt> GetEnderecos()
        {
            throw new NotImplementedException();
        }

        public List<EnderecoSt> GetEnderecos(string selector)
        {
            throw new NotImplementedException();
        }

        public EnderecoSt GetEnderecos(int id)
        {
            throw new NotImplementedException();
        }
    }
}
