using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class EntrarViewModel  //LoginViewModel
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "formato de e-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DisplayName("Palavra passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegistrarViewModel
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "formato de e-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres de longitude.", MinimumLength = 6)]
        [DisplayName("Palavra passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "A palavra passe e a sua confirmção diferem")]
        [DisplayName("Confirmar Palavra passe")]
        [DataType(DataType.Password)]
        public string ConfirmaPassword { get; set; }

        [DisplayName("Li e aceito os termos e condições")]
        public bool Termos { get; set; }

    }

    public class EsqueciChaveViewModel
    {
        [Required(ErrorMessage = "Entre o E-mail")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "formato de e-mail inválido")]
        public string Email { get; set; }
    }
}