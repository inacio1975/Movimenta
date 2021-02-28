using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class MovimentadorRepository : IMovimentadorRepository
    {
        public void AddMovimentador(Movimentador movimentador)
        {
            throw new NotImplementedException();
        }

        public bool EliminBanco(int id)
        {
            throw new NotImplementedException();
        }

        public List<Movimentador> GetMovimentadores()
        {
            throw new NotImplementedException();
        }

        public List<Movimentador> GetMovimentadores(Projecto projecto)
        {
            IEnumerable<Movimentador> busca = new List<Movimentador>();
            try
            {
                using (var context = new MovimentaContext())
                {
                    //busca = context.Movimentadores.Where(x => x.Projecto.ProjectoId == projecto.ProjectoId);
                    busca = from fila in context.Movimentadores where fila.Projecto.ProjectoId == projecto.ProjectoId select fila;
                    if (busca.Any())
                        return busca.ToList();
                }
            }
            catch (Exception e)
            {

            }
            return new List<Movimentador>();
        }

        public List<Movimentador> GetMovimentadores(string selector)
        {
            throw new NotImplementedException();
        }

        public Movimentador GetMovimentador(int id)
        {
            throw new NotImplementedException();
        }
    }
}
