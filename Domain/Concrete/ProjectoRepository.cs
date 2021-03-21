using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class ProjectoRepository : IProjectoRepository
    {
        public void AddProjecto(Projecto projecto)
        {
            using (var context = new MovimentaContext())
            {
                context.Projectos.Add(projecto);
                context.SaveChanges();
            }
        }

        public void AddNovidade(Projecto projecto, Novidade novidade)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Elimina um projecto da fonte de dados dado o seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True se foi possivel eliminar o elemento</returns>
        public bool EliminaProjecto(int id)
        {
            try
            {
                using (var context = new MovimentaContext())
                {
                    var sysQuery = (from projecto in context.Projectos
                                    where projecto.ProjectoId == id
                                    select projecto).Single();
                    context.Projectos.Remove(sysQuery);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Projecto> GetProjectos()
        {
            using (var context = new MovimentaContext())
            {
                return context.Projectos.ToList();
            }
        }

        /// <summary>
        /// Localiza um Projecto tentando varios parametros de busca
        /// </summary>
        /// <param name="selector">El o parametro para efdectuar a comparação</param>
        /// <returns>A lista dos projectos encontrados</returns>
        public List<Projecto> GetProjectos(string selector)
        {
            using (var context = new MovimentaContext())
            {
                var busca = from projecto in context.Projectos select projecto;
                if (!string.IsNullOrEmpty(selector))
                {
                    busca =
                        busca.Where(
                            s =>
                                s.Descricao.Contains(selector) || s.Sobre.Contains(selector) ||
                                s.Criador.Nome.Contains(selector) || s.Keywords.Contains(selector));
                }
                return busca.ToList();
            }
        }

        public List<Projecto> GetRelatedProjects(Projecto projecto)
        {
            using (var context = new MovimentaContext())
            {
                var projectos = context.Projectos.Where(p => p.MembroId == projecto.MembroId).ToList();
                
                return projectos;
            }
        }

        public List<Projecto> GetPopularProjects()
        {
            using (var context = new MovimentaContext())
            {
                var projectos = context.Projectos.ToList().OrderByDescending(p => p.GetMovimentadores().Count).ToList();


                return projectos;
            }
        } 

        public List<Projecto> GetProjectosWithPage(int start, int productPerPage)
        {
            using (var context = new MovimentaContext())
            {
                var projectos = context.Projectos.ToList()
                                                 .OrderBy(p => p.ProjectoId)
                                                 .Skip((start-1)*productPerPage)
                                                 .Take(productPerPage).ToList();
                return projectos;
            }
        }

        /// <summary>
        /// Localiza um Projecto pelo seu ID
        /// </summary>
        /// <param name="id">El Identificador do Projecto</param>
        /// <returns>O
        ///  Projecto localizado</returns>
        public Projecto GetProjectos(int id)
        {
            using (var context = new MovimentaContext())
            {
                return (from projecto in context.Projectos
                        where projecto.ProjectoId == id
                        select projecto).SingleOrDefault();
            }
        }

        public List<Projecto> GetProjectos(Membro membro)
        {
            using (var context = new  MovimentaContext())
            {
                return context.Projectos.Where(projecto => projecto.MembroId == membro.MembroId).Distinct().ToList();
            }
        }

        public List<Projecto> GetProjectosByCategory(int categoriaId)
        {
            using (var context = new MovimentaContext())
            {
                return context.Projectos.Where(projecto => projecto.CategoriaId == categoriaId).Distinct().ToList();
            }
        }

        List<Comentario> IProjectoRepository.GetComentarios(Projecto projecto)
        {
            throw new NotImplementedException();
        }

        List<Novidade> IProjectoRepository.GetNovidades(Projecto projecto)
        {
            throw new NotImplementedException();
        }

        public ICollection<Comentario> GetComentarios(Projecto projecto)
        {
            throw new NotImplementedException();
        }

        public ICollection<Novidade> GetNovidades(Projecto projecto)
        {
            throw new NotImplementedException();
        }
    }
}
