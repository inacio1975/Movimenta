using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Campanha
    {
        [Key]
        public int CapanhaId { get; set; }

        public string Nome { get; set; }
    }
}
