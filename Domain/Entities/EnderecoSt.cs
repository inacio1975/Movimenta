using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class EnderecoSt
    {
        [Key]
        public int Id { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public int StartupId { get; set; }
        public Startup Startup { get; set; }
    }
}