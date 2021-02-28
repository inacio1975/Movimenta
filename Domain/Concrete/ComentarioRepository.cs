using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class ComentarioRepository : IComentarioRepository
    {
        public void AddComentario(Comentario comentario)
        {
            throw new NotImplementedException();
        }

        public bool EliminComentario(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comentario> GetComentarios()
        {
            throw new NotImplementedException();
        }

        public List<Comentario> GetComentarios(string selector)
        {
            throw new NotImplementedException();
        }

        public List<Comentario> GetComentarios(Projecto projecto)
        {
            IEnumerable<Comentario> busca = new List<Comentario>();
            try
            {
                using (var context = new MovimentaContext())
                {
                    //busca = context.Comentarios.Where(x => x.Projecto.ProjectoId == projecto.ProjectoId);
                    busca = from fila in context.Comentarios where fila.GetProjecto().ProjectoId == projecto.ProjectoId select fila;
                    if (busca.Any() != false)
                        return busca.ToList();
                }
            }
            catch (Exception)
            {
                
            }
            return new List<Comentario>();
        }

        public Comentario GetComentarios(int id)
        {
            throw new NotImplementedException();
        }
    }
}
