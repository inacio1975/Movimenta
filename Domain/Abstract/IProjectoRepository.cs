using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IProjectoRepository
    {
        void AddProjecto(Projecto projecto);
        void AddNovidade(Projecto projecto, Novidade novidade);
        bool EliminaProjecto(int id);

        List<Projecto> GetProjectos();
        List<Projecto> GetProjectos(string selector);
        Projecto GetProjectos(int id);
        List<Projecto> GetProjectosByCategory(int categoriaId);
        List<Comentario> GetComentarios(Projecto projecto);
        List<Novidade> GetNovidades(Projecto projecto);
    }
}
