using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MembroUsuario
    {
        [Key]
        public int id { get; set; }

        public int MembroId { get; set; }
        public Membro Membro { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
