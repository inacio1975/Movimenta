using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IEnderecoRepository
    {
        void AddEndereco(Endereco enderecoMb);
        bool EliminEndereco(int id);

        List<Endereco> GetEnderecos();
        List<Endereco> GetEnderecos(string selector);
        Endereco GetEnderecos(int id);
    }
}
