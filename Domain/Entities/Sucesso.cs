using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Concrete;

namespace Domain.Entities
{
    public class Sucesso
    {
        [Key]
        public int SucessoId { get; set; }
        [Required(ErrorMessage = "A opinião tem que ser inserida")]
        [DisplayName("Opinião")]
        public string Opiniao { get; set; }
        [DisplayName("Data")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "É necessário definir o membro")]
        [DisplayName("Membro")]
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
    }
}
