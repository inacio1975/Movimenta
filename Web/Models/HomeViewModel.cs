using System.Collections.Generic;
using System.Linq;
using Domain.Concrete;
using Domain.Entities;

namespace Web.Models
{
    public class HomeViewModel
    {
        public Membro Membro;

        public IEnumerable<Projecto> Projectos { get; set; }
        public List<Sucesso> Sucessos { get; set; }
        public List<Categoria> Categorias { get; set; } 
    }

    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Foto { get; set; }

        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Webpage { get; set; }
        public string Citacao { get; set; }
    }
}