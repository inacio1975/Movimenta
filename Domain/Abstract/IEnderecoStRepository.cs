using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IEnderecoStRepository
    {
        void AddEndereco(EnderecoSt enderecoMb);
        bool EliminEndereco(int id);

        List<EnderecoSt> GetEnderecos();
        List<EnderecoSt> GetEnderecos(string selector);
        EnderecoSt GetEnderecos(int id);
    }
}
