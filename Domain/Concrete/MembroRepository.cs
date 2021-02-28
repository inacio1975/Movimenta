using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class MembroRepository : IMembroRepository
    {
        public void AddMembro(Membro membro)
        {
            using (var context = new MovimentaContext())
            {
                context.Membros.Add(membro);
                context.SaveChanges();
            }
        }

        public bool EliminaMembro(int id)
        {
            try
            {
                using (var context = new MovimentaContext())
                {
                    var sysQuery = (from fila in context.Membros
                                    where fila.MembroId == id
                                    select fila).Single();
                    context.Membros.Remove(sysQuery);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Membro> GetMembros()
        {
            using (var context = new MovimentaContext())
            {
                return context.Membros.ToList();
            }
        }

        public List<Membro> GetMembros(string selector)
        {
            using (var context = new MovimentaContext())
            {
                var busca = from fila in context.Membros select fila;
                if (!string.IsNullOrEmpty(selector))
                {
                    busca =
                        busca.Where(
                            s =>
                                s.Nome.Contains(selector));
                }
                return busca.ToList();
            }
        }

        public Membro GetMembro(int id)
        {
            using (var context = new MovimentaContext())
            {
                return (from fila in context.Membros
                        where fila.MembroId == id
                        select fila).SingleOrDefault();
            }
        }
    }
}
