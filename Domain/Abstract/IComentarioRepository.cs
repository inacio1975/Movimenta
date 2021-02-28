using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IComentarioRepository
    {
        void AddComentario(Comentario comentario);
        bool EliminComentario(int id);

        List<Comentario> GetComentarios();
        List<Comentario> GetComentarios(Projecto projecto);
        List<Comentario> GetComentarios(string selector);
        Comentario GetComentarios(int id);
    }
}
