using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }

        public int IdPais { get; set; }
        public Pais Pais { get; set; }

        public int? IdProvincia { get; set; }
        public Provincia Provincia { get; set; }

        public int? IdMunicipio { get; set; }
        public Municipio Municipio { get; set; }

        [Required]
        public string Rua { get; set; }
    }
}
