using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IEnderecoMbRepository
    {
        void AddEndereco(EnderecoMb enderecoMb);
        bool EliminEndereco(int id);

        List<EnderecoMb> GetEnderecos();
        List<EnderecoMb> GetEnderecos(string selector);
        EnderecoMb GetEnderecos(int id);
    }
}
