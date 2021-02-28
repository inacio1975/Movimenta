using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RoleUsuario
    {
        [Key]
        public int Id { get; set; }
        public Guid UsuarioId { get; set;}
        public Usuario Usuario { get; set; }
        public int RolId { get; set;}
        public Rol Rol { get; set; }
    }
}
