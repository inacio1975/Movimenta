using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class ContactoMb
    {
        [Key]
        public int Id { get; set; }

        public int MembroId { get; set; }
        public Membro Membro { get; set; }

        public int ContactoId { get; set; }
        public Contacto Contacto { get; set; }

    }
}