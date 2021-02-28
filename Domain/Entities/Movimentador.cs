using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movimentador
    {
        [Key]
        public int IdMovimentador { get; set; }

        [Required(ErrorMessage = "Membro necessário")]
        [DisplayName("Membro")]
        public int IdMembro { get; set; }
        public Membro Membro { get; set; }

        [Required(ErrorMessage = "Projecto necessário")]
        [DisplayName("Projecto")]
        public int ProjectoId { get; set; }
        public Projecto Projecto { get; set; }

        [Required(ErrorMessage = "Quantia necessária")]
        [DisplayName("Valor")]
        public float Valor { get; set; }

        [Required(ErrorMessage = "Data obrigatória")]
        [DisplayName("Data")]
        public DateTime Data { get; set; }
    }
}
