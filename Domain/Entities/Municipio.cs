using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Municipio
    {
        [Key]
        public int IdMunicipio { get; set; }
        [Required]
        public string Nome { get; set; }


        public int? IdProvincia { get; set; }
        public Provincia Provincia { get; set; }
    }
}
