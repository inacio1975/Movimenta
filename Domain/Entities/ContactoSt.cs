using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class ContactoSt
    {
        [Key]
        public int Id { get; set; }

        public int StartupId { get; set; }
        public Startup Startup { get; set; }

        public int ContactoId { get; set; }
        public Contacto Contacto { get; set; }
    }
}