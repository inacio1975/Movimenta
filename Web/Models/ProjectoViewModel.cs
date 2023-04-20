using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
using Domain.Entities;

namespace Web.Models
{
    public class CriarViewModel
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("Titulo")]
        public string Titulo { get; set; }

        public IEnumerable<SelectListItem> Campanha { get; set; }
        [DisplayName("Tipo de campanha")]
        public string CampanhaSelecionada { get; set; }
    }

    public class EditarViewModel
    {
        public int ProjectoId { get; set; }

        [DisplayName("Titulo")]
        public string Titulo { get; set; }

        [DisplayName("Descricao")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [DisplayName("Sobre")]
        [DataType(DataType.MultilineText)]
        public string Sobre { get; set; }

        public IEnumerable<SelectListItem> Categoria { get; set; }
        [DisplayName("Categoria")]
        public string CategoriaSelecionada { get; set; }

        [DisplayName("Palavras chaves")]
        [DataType(DataType.Text)]
        public string Keywords { get; set; }

        [DisplayName("Foto")]
        [DataType(DataType.ImageUrl)]
        public string Foto { get; set; }

        public HttpPostedFileBase ficherofoto { get; set; }


        [DisplayName("Video")]
        [DataType(DataType.Text)]
        public string Video { get; set; }

        [DisplayName("Tempo de Arrecadação")]
        public int DiasArrecadacao { get; set; }

        [DisplayName("Meta")]
        public Double? Meta { get; set; }

        [DisplayName("Valor Mínimo")]
        public Double? Minimo { get; set; }

        [DisplayName("Valor Recomendado")]
        public Double? Recomendado { get; set; }

        [DisplayName("Objectivo dos fundos")]
        [DataType(DataType.MultilineText)]
        public string Objectivofundo { get; set; }

        public IEnumerable<SelectListItem> Campanha { get; set; }
        [DisplayName("Tipo de campanha")]
        public string CampanhaSelecionada { get; set; }

        public Estado? Estado { get; set; }

        [DisplayName("País")]
        public string PaisSelecionado { get; set; }
        public IEnumerable<SelectListItem> Pais { get; set; }

        [DisplayName("Provincia")]
        public string ProvinciaSelecionada { get; set; }
        public IEnumerable<SelectListItem> Provincia { get; set; }

        [DisplayName("Municipio")]
        public string MunicipioSelecionado { get; set; }
        public IEnumerable<SelectListItem> Municipio { get; set; }

        [DisplayName("Endereço")]
        public string Rua { get; set; }

        [DisplayName("Doar apartir de")]
        public double ValorDoado { get; set; }

        [DisplayName("Recompensa")]
        public string Item { get; set; }

        [DisplayName("Descrição")]
        [DataType(DataType.MultilineText)]
        public string RecDescricao { get; set; }

        [DisplayName("Quantidade")]
        public double Quantidade { get; set; }

        [DisplayName("Data de entrega estimada")]
        public string PrazoEntregaEstimado { get; set; }

        [DisplayName("Entregado em")]
        public string LugarEntrega { get; set; }

        public IEnumerable<RecompensaViewModel> Recompensas { get; set; }

        public EditarViewModel()
        {
            Recompensas = new List<RecompensaViewModel>();
        }
    }

    public class DetalheViewModel
    {
        public int ProjectoId { get; set; }
        public string Titulo { get; set; }
        public string Video { get; set; }
        public string Keywords { get; set; }
        public string FotoUrl { get; set; }

        public string DescricaoCurta { get; set; }
        public string SobreCampanha { get; set; }
        public string UsoFundo { get; set; }

        public IEnumerable<Novidade> Novidades { get; set; }
        public IEnumerable<Comentario> Comentarios { get; set; }
        public IEnumerable<Movimentador> Movimentadores { get; set; }  

        public int Progresso { get; set; }
        public double Arrecadado { get; set; }
        public double Meta { get; set; }
        public DateTime DataFim { get; set; }
        public DateTime DataInicio { get; set; }
        public string LinkFb { get; set; }
        public string LinkTt { get; set; }

        public Membro Autor { get; set; }

        public IEnumerable<Recompensa> Recompensas { get; set; } 
        public List<Projecto> Projectos { get; set; }


        public string GetAuthorAddress()
        {
            var endereco = new EnderecoMbRepository().GetEndereco(Autor);

            return endereco?.ToString() ?? "Enderço não definido";
        }
    }

    public class RecompensaViewModel
    {
        public int Id { get; set; }

        [DisplayName("Doar apartir de")]
        public double ValorDoado { get; set; }

        [DisplayName("Recompensa")]
        public string Item { get; set; }

        [DisplayName("Descrição")]
        public string RecDescricao { get; set; }

        [DisplayName("Quantidade")]
        public double Quantidade { get; set; }

        [DisplayName("Data de entrega estimada")]
        public string PrazoEntregaEstimado { get; set; }

        [DisplayName("Entregado em")]
        public string LugarEntrega { get; set; }
    }

    public class ComentarioViewModel
    {
        public int ProjectoId { get; set; }
        public int MembroId { get; set; }

        public string Nome { get; set; }
        public string Data { get; set; }
        public string Foto { get; set; }
        public  string Comentario { get; set; }
    }

    public class CampanhaViewModel
    {
        public List<Projecto> Projectos { get; set; }
        public Paginator paginator { get; set; }

        public class Paginator
        {
            public int start { get; }
            public double pages { get; }

            private static int PROJECTPERPAGE = 6;

            public Paginator(int page, List<Projecto> projectos)
            {
                start = page;
                pages = Math.Ceiling(projectos.Count * 1.0 / PROJECTPERPAGE);
            }
        } 
    }


}