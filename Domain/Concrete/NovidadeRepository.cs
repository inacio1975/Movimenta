using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class NovidadeRepository : INovidadeRepository
    {
        public void AddNovidade(Novidade Novidade)
        {
            throw new NotImplementedException();
        }

        public bool EliminNovidade(int id)
        {
            throw new NotImplementedException();
        }

        public List<Novidade> GetNovidades()
        {
            using (var context = new MovimentaContext())
            {
                return context.Novidades.ToList();
            }
        }

        public List<Novidade> GetNovidades(string selector)
        {
            throw new NotImplementedException();
        }


        public Novidade GetNovidades(int id)
        {
            throw new NotImplementedException();
        }

        public List<Novidade> getNovidadePrj(int prj)
        {
            IEnumerable<Novidade> busca = new List<Novidade>();
            try
            {
                using (var context = new MovimentaContext())
                {
                    //busca = context.Novidades.Where(x => x.Projecto.ProjectoId == prj);
                    busca = from fila in context.Novidades where fila.GetProjecto().ProjectoId == prj select fila;
                    if (busca.Any() != false)
                        return busca.ToList();
                }
            }
            catch (Exception e)
            {

            }
            return new List<Novidade>();
        }
    }
}