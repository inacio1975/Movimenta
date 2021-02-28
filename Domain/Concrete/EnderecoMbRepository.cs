using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EnderecoMbRepository : IEnderecoMbRepository
    {
        public void AddEndereco(EnderecoMb enderecoMb)
        {
            throw new NotImplementedException();
        }

        public bool EliminEndereco(int id)
        {
            throw new NotImplementedException();
        }

        public List<EnderecoMb> GetEnderecos()
        {
            throw new NotImplementedException();
        }

        public List<EnderecoMb> GetEnderecos(string selector)
        {
            throw new NotImplementedException();
        }

        public EnderecoMb GetEnderecos(int id)
        {
            throw new NotImplementedException();
        }
    }
}
