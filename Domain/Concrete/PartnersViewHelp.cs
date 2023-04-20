using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class PartnersViewHelp
    {
        public int PartnerId { get; set; }
        public string Entidade { get; set; }
        public string Logo { get; set; }
        public string Link { get; set; }
    }

    public class MetodoPagamentoViewHelp
    {
        public MetodoPagamento PagamentoId { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }

        public enum MetodoPagamento { Transferencias = 1, Ekawanza, Aki,Paykwz }
    }
}
