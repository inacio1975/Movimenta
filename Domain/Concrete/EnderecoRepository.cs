using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public void AddEndereco(Endereco enderecoMb)
        {
            throw new NotImplementedException();
        }

        public bool EliminEndereco(int id)
        {
            throw new NotImplementedException();
        }

        public List<Endereco> GetEnderecos()
        {
            throw new NotImplementedException();
        }

        public List<Endereco> GetEnderecos(string selector)
        {
            throw new NotImplementedException();
        }

        public Endereco GetEnderecos(int id)
        {
            throw new NotImplementedException();
        }
    }
}
