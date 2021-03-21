using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EnderecoMbRepository : IEnderecoMbRepository
    {
        public void AddEndereco(EnderecoMb enderecoMb)
        {
            using (var context = new MovimentaContext())
            {
                context.EnderecoDosMembros.Add(enderecoMb);
                context.SaveChanges();
            }
        }

        public bool EliminEndereco(int id)
        {
            try
            {
                using (var context = new MovimentaContext())
                {
                    var address = (from endereco in context.EnderecoDosMembros
                        where endereco.EnderecoId == id
                        select endereco).Single();
                    context.EnderecoDosMembros.Remove(address);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<EnderecoMb> GetEnderecos()
        {
            using (var context = new MovimentaContext())
            {
                return context.EnderecoDosMembros.ToList();
            }
        }

        public List<EnderecoMb> GetEnderecos(string selector)
        {
            throw new NotImplementedException();
        }

        public EnderecoMb GetEndereco(Membro author)
        {
            using (var context = new MovimentaContext())
            {
                var address = (from endereco in context.EnderecoDosMembros
                    where endereco.MembroId == author.MembroId
                    select endereco).SingleOrDefault();
                return address;
            }
        }
    }
}
