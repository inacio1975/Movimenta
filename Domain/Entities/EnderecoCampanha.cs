using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Concrete;

namespace Domain.Entities
{
    public class EnderecoCampanha
    {
        [Key]
        public int IdLocalizacao { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public EnderecoCampanha()
        {
            var db = new MovimentaContext();
            Endereco = db.Enderecos.Find(EnderecoId);
        }
    }
}
