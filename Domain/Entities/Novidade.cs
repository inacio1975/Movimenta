using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Concrete;

namespace Domain.Entities
{
    public class Novidade
    {
        [Key]
        public int NovidadeId { get; set; }
        [Required(ErrorMessage = "Insere o conteúdo")]
        [DisplayName("qual é a novidade?")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Insere o conteúdo")]
        [DisplayName("Novidade")]
        public string Descricao { get; set; }

        [DisplayName("Data")]
        public DateTime Data { get; set; }

        //[Required(ErrorMessage = "Criador não especificado")]
        //[DisplayName("Criador")]
        //public int MembroId { get; set; }
        //public Membro Membro { get; set; }

        //[Required(ErrorMessage = "Projecto não especificado")]
        [DisplayName("Projecto")]
        public int ProjectoId { get; set; }
        //public Projecto Projecto { get; set; }

        public Projecto GetProjecto()
        {
            return new MovimentaContext().Projectos.Find(ProjectoId);
        }
    }
}