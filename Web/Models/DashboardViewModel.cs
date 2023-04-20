using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Activation;

namespace Web.Models
{
    public class SegurancaViewModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; } 
    }

    public class CategoriaViewModel
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public int NumProjectos { get; set; }
    }

    public class UsuarioViewModel
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Organizacao { get; set; }
        public List<IdentityRole> Roles { get; set; } 
        public string Foto { get; set; }

        public string GeRoles()
        {
            return Roles.Aggregate("", (current, rol) => current + (" " + rol.Name));
        }

        public override string ToString()
        {
            return Nome + " | " + ID;
        }
    }

    public class EventoViewModel
    {
        public int EventoId { get; set; }
        public string Titulo { get; set; }
        public string FotoUrl { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoLarga { get; set; }
        public string Local { get; set; }
        public DateTime Data { get; set; }
    }

    public class ProjectoViewModel
    {
        public int ProjectoId { get; set; }
        public string Titulo { get; set; }
    }
}