using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IMovimentadorRepository
    {
        void AddMovimentador(Movimentador movimentador);
        bool EliminBanco(int id);

        List<Movimentador> GetMovimentadores();
        List<Movimentador> GetMovimentadores(Projecto projecto);
        List<Movimentador> GetMovimentadores(string selector);
        Movimentador GetMovimentador(int id);
    }
}
