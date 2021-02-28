using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Recompensa
    {
        [Key]
        public int IdRecompensa { get; set; }

        public double ValorDoado { get; set; }
        public double Quantidade { get; set; }
        public string Item { get; set; }
        public string Descricao { get; set; }
        public string PrazoEntregaEstimado { get; set; }
        public string LugarEntrega { get; set; }

        public int ProjectoId { get; set; }
        public Projecto Projecto { get; set; }
    }
}
