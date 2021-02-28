using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void AddUsuario(Usuario usuario)
        {
            using (var context = new MovimentaContext())
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
        }

        public List<Usuario> GetUsuarios()
        {
            using (var context = new MovimentaContext())
            {
                return context.Usuarios.ToList();
            }
        }

        public Usuario GeUsuario(Guid usuarioId)
        {
            throw new NotImplementedException();
        }

        public Usuario GeUsuarioByEmail(string email)
        {
            using (var context = new MovimentaContext())
            {
                var busca = from usuario in context.Usuarios select usuario;
                if (!string.IsNullOrEmpty(email))
                {
                    busca =
                        busca.Where(s => s.Email.Equals(email));
                }
                return busca.Single();
            }

        }
    }
}

