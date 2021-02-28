using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }
        public string Titulo { get; set; }
        public string FotoUrl { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoLarga { get; set; }
        public string Local { get; set; }
        public DateTime? Data { get; set; }
    }
}
