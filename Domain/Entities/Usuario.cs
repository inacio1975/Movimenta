using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Usuario
    {
        [Key]
        //[DefaultValue(Guid.newG)]
        public Guid UsuarioId { get; set; }
        [Required]
        public string Email { get; set; }
        public bool EmailConfirmado { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public bool TelefoneConfirmado { get; set; }
        public int AcessoFailedCount { get; set; }
        [Required]
        public DateTime DataRegistro { get; set; }
    }
}
