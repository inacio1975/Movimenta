using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        public string nome { get; set; }
    }
}
