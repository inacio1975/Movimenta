using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    //[Table("Bancos")]
    public class Banco
    {
        [Key]
        public int BancoId { get; set; }
        [Required(ErrorMessage = "Numero de conta obrigatório")]
        [DisplayName("Conta Bancária")]
        public string Conta { get; set; }
        [Required(ErrorMessage = "O IBAN é obrigatório")]
        [DisplayName("IBAN")]
        public string Iban { get; set; }

        //public List<Startup> Startups { get; set; }
    }
}
