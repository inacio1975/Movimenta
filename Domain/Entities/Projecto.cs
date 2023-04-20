using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Domain.Concrete;

namespace Domain.Entities
{
    public enum Estado { Rascunho, Analise, Aprovado, Publicado,Terminado, Completado ,Ocultado}

    public class Projecto
    {
        [Key]
        public int ProjectoId { get; set; }
        
        [Required(ErrorMessage = "Titulo requerido")]
        [DisplayName("Titulo")]
        public string Titulo { get; set; }
        //public int StartupId { get; set; }
        [DisplayName("Foto")]
        [DataType(DataType.ImageUrl)]
        public string Foto { get; set; }
        [DisplayName("Video")]
        [DataType(DataType.ImageUrl)]
        public string Video { get; set; }
        //[Required(ErrorMessage = "A descrição é requerida")]
        [DisplayName("Descricao")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }
        [DisplayName("Sobre")]
        [DataType(DataType.MultilineText)]
        public string Sobre { get; set; }
        [DisplayName("Objectivo dos fundos")]
        public string Objectivofundo { get; set; }
       
        [DisplayName("Palavras chaves")]
        [DataType(DataType.Text)]
        public string Keywords { get; set; }
        
        [DisplayName("Meta")]
        public Double? Meta { get; set; }
        [DisplayName("Valor Arrecadado")]
        public Double? Arrecadado { get; set; }
        [DisplayName("Data de inicio")]
        public DateTime? DataInicio { get; set; }
        [DisplayName("Data do fim")]
        public DateTime? DataFim { get; set; }
        [DisplayName("Duração da campanha")]
        public int? Duracao { get; set; }

        [DisplayName("Tipo de campanha")]
        public int? CapanhaId { get; set; }
        public Campanha Campanha { get; set; }

        [DisplayName("Categoria")]
        public int? CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public Estado? Estado { get; set; }

        [Required(ErrorMessage = "Membro requerido")]
        [DisplayName("Membro ID")]
        public int MembroId { get; set; }
        public Membro Criador { get; set; }

        public int? IdLocalizacao { get; set; }
        public EnderecoCampanha EnderecoCampanha { get; set; }

        public Projecto()
        {
            var db = new MovimentaContext();

            Campanha = db.Campanhas.Find(CapanhaId);
            Categoria = db.Categorias.Find(CategoriaId);
            Criador = db.Membros.Find(MembroId);
            EnderecoCampanha = db.EnderecoCampanhas.Find(IdLocalizacao);
        }

        public Endereco GetEnderecoCampanha()
        {
            var db = new MovimentaContext();
            var endrcamp = db.EnderecoCampanhas.Find(IdLocalizacao);

            return endrcamp != null ? db.Enderecos.Find(endrcamp.EnderecoId) : new Endereco();
        }

        public int GetProgresso()
        {
            var arrecadado = Arrecadado ?? 0;
            var meta = Meta ?? 0;

            var progresso = Math.Abs(meta) > 0 ? arrecadado / meta :  0;
            return Convert.ToInt16(progresso * 100);
        }

        public IEnumerable<string> GetKeywords()
        {
            return Keywords?.Split(',').ToList() ?? new List<string>();
        }

        public Membro GetCriador()
        {
            return new MovimentaContext().Membros.Find(MembroId);
        }

        public int DiasRestantes()
        {
            var timeSpan = DataFim - DateTime.Now;
            return timeSpan?.Days ?? 0;
        }

        public int TempoArrecadacao()
        {
            var timeSpan = DataFim - DataInicio;
            return timeSpan?.Days ?? 0;
        }

        public List<Novidade> GetNovidades()
        {
            return new NovidadeRepository().getNovidadePrj(ProjectoId);
        }

        public List<Comentario> GetComentarios()
        {
            return  new ComentarioRepository().GetComentarios(this);
        }

        public List<Movimentador> GetMovimentadores()
        {
            return new MovimentadorRepository().GetMovimentadores(this);
        }

        public string GetCategoria()
        {
            var categoria = new MovimentaContext().Categorias.Find(CategoriaId);
            return categoria?.nome;
        }

        public IEnumerable<Recompensa> GetRecompensas()
        {
            return new MovimentaContext().Recompensas.Where(rec => rec.ProjectoId == ProjectoId);   
        } 
    }
}