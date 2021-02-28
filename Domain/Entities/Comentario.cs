using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Concrete;

namespace Domain.Entities
{
    public class Comentario
    {
        [Key]
        public int ComentarioId { get; set; }
        
        [Required(ErrorMessage = "Não inseriu o comentário")]
        [DisplayName("Comentário")]
        public string comentario { get; set; }
        [Required(ErrorMessage = "Não especificou a data do comentário")]
        [DisplayName("Data")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Usuario não especificado")]
        [DisplayName("Usuário")]
        public int MembroId { get; set; }
        public Membro Membro { get; set; }

        //[Required(ErrorMessage = "Projecto não especificado")]
        [DisplayName("Projecto")]
        public int ProjectoId { get; set; }
        //public Projecto Projecto { get; set; }

        public Projecto GetProjecto()
        {
            return new MovimentaContext().Projectos.Find(ProjectoId);
        }

        public Membro GetMembro()
        {
            return new MovimentaContext().Membros.Find(MembroId);
        }
    }
}