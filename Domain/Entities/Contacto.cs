using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contacto
    {
        [Key]
        public int ContactoId { get; set; }
        [Required(ErrorMessage = "Telefone não especificado")]
        [DisplayName("Telefone 1")]
        public string Tel1 { get; set; }
        [DisplayName("Telefone 2")]
        public string Tel2 { get; set; }
        [Required(ErrorMessage = "Email não especificado")]
        [DisplayName("Email")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
