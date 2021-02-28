using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IUsuarioRepository
    {
        void AddUsuario(Usuario usuario);

        List<Usuario> GetUsuarios();

        Usuario GeUsuario(Guid usuarioId);

        Usuario GeUsuarioByEmail(string email);

    }
}
