using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DocumentoMembro
    {
        [Key]
        public int Id { get; set; }

        public int DocumentoId { get; set; }
        public Documento Documento { get; set; }

        public int MembroId { get; set; }
        public Membro Membro { get; set; }

    }
}
