using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pais
    {
        [Key]
        public int IdPais { get; set; }
        [Required]
        public string Nome{ get; set; }
    }
}
