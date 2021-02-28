using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class EnderecoMb
    {
        [Key]
        public int Id { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public int MembroId { get; set; }
        public Membro Membro { get; set; }

    }
}