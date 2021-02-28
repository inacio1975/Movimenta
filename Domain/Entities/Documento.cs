using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class Documento
    {
        [Key]
        public int DocumentoId { get; set; }
        [Required(ErrorMessage = "Tipo do docuemnto Requerido")]
        [DisplayName("Tipo do documento")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Número do documento não especificado")]
        [DisplayName("Número do documento")]
        public string Numero { get; set; }
    }
}