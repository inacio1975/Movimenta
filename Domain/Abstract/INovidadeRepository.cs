using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface INovidadeRepository
    {
        void AddNovidade(Novidade Novidade);
        bool EliminNovidade(int id);

        List<Novidade> GetNovidades();
        List<Novidade> GetNovidades(string selector);
        Novidade GetNovidades(int id);
        List<Novidade> getNovidadePrj(int prj);
    }
}
