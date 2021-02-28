using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface ISucessoRepository
    {
        void AddSucesso(Sucesso sucesso);
        bool EliminaSucesso(int id);

        List<Sucesso> GetSucessos();
    }
}
