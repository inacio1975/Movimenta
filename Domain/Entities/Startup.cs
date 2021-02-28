using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class Startup
    {
        [Key]
        public virtual int StartupId { get; set; }
        
        [Required(ErrorMessage = "Nome não especificado")]
        [DisplayName("Nome")]
        public virtual string Nome { get; set; }
        [DisplayName("Foto")]
        public virtual string Foto { get; set; }
        [Required(ErrorMessage = "Área de actuação não especificada")]
        [DisplayName("Área de actuação")]
        public virtual string AreaActuacao { get; set; }

        [DisplayName("Corrdenas bancárias")]
        public virtual  int? BancoId { get; set; }
        public virtual Banco Banco { get; set; }

        //public virtual List<Membro> Membros { get; set; }
        public virtual List<Projecto> Projectos { get; set; }
    }
}