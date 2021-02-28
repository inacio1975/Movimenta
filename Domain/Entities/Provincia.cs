using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Provincia
    {
        [Key]
        public int IdProvincia { get; set; }
        [Required]
        public string Nome { get; set; }

        public int IdPais { get; set; }
        public Pais Pais { get; set; }
    }
}
