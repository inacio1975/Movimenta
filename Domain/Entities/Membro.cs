using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Concrete;

namespace Domain.Entities
{
    public class Membro
    {
        [Key]
        public int MembroId { get; set; }
        
        [Required(ErrorMessage = "O nome não foi especificado")]
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [DisplayName("Foto")]
        public string Foto { get; set; }
        [DisplayName("Cargo")]
        public string Cargo { get; set; }

        public string Bio { get; set; }

        public virtual int? StartupId { get; set; }
        public Startup Startup { get; set; }

        public Startup GetStartup()
        {
            return new StartupRepository().GetStartup(StartupId??1);
        }

    }
}