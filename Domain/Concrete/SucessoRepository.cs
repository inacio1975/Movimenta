using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class SucessoRepository : ISucessoRepository
    {
        public void AddSucesso(Sucesso sucesso)
        {
            using (var context = new MovimentaContext())
            {
                context.Sucessos.Add(sucesso);
                context.SaveChanges();
            }
        }

        public bool EliminaSucesso(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sucesso> GetSucessos()
        {
            using (var context = new MovimentaContext())
            {
                return context.Sucessos.Take(2).ToList();
            }
        }
    }
}
