using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace Web.Models
{
    public class UsuarioPerfilViewModel
    {
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "formato de e-mail inválido")]
        public string Email { get; set; }

        [DisplayName("Nome")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        //[FileExtensions(Extensions = "jpeg",ErrorMessage = "Arquivo inválido. Escolha uma imagem jpg")]
        [DisplayName("Foto do perfil")]
        public HttpPostedFileBase Foto { get; set; }

        [DisplayName("Organização")]
        [DataType(DataType.Text)]
        public string Startup { get; set; }

        [DisplayName("Cargo")]
        [DataType(DataType.Text)]
        public string Cargo { get; set; }

        [DisplayName("Sobre Mim")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }
    }

    public class ProjectosCriadosViewModel
    {
        public int Id { get; set; }

        [DisplayName("Titulo")]
        [DataType(DataType.Text)]
        public string Titulo { get; set; }

        [DisplayName("Autor")]
        [DataType(DataType.Text)]
        public string Autor { get; set; }

        public Estado? Estado { get; set; }

        public double? Arrecadado { get ; set ; }
        public double? Meta { get; set; }

        public int DiasRestantes { get; set; }
    }

    public class DadosViewModel
    {
        [DisplayName("País")]
        public List<SelectListItem> Pais { get; set; }

        [DisplayName("Provincia")]
        public List<SelectListItem> Provincia { get; set; }

        [DisplayName("Municipio")]
        public List<SelectListItem> Municipio { get; set; }

        [DisplayName("Rua")]
        public string Rua { get; set; }

    }
}