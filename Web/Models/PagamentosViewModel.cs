using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace Web.Models
{
    public class EscolherValorViewModel
    {
        public double Valor { get; set; }

        public int RecompensaId { get; set; }
        public int PagamentoSelecionado { get; set; }
        public Recompensa Recompensa { get; set; }

        public int ProjectoId { get; set; }
        public Projecto Projecto { get; set; }

        public IEnumerable<Recompensa> Recompensas { get; set; }
    }

    public class DetalhePagamentoViewModel
    {
        public int  PagamentoSelecionado { get; set; }
        public double Valor { get; set; }

        public Membro Movimentador { get; set; }
        public Projecto Projecto { get; set; }
        public Recompensa Recompensa { get; set; }


    }
}