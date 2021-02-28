using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Carrossel
    {
        [Key]
        public int CarrosselId { get; set; }
        public int TipoCarrossel { get; set; }
        public string Foto { get; set; }
    }

}
