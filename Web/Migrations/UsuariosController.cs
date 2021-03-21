using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        MovimentaContext context = new MovimentaContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private ApplicationUser GetUserLogedIn()
        {
            var user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            return user;
        }

        public UsuariosController()
        {
            try
            {
                ViewBag.Membro = GetUserLogedIn().Membro;
            }
            catch (NullReferenceException e)
            {

            }
        }

        public UsuariosController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }


        // GET: Usuarios/Perfil
        public ActionResult Perfil()
        {
            var user = getUserLogedIn();
            var membro = user.Membro;
            var startup = membro.GetStartup();
            var perfil = new UsuarioPerfilViewModel
            {
                Email = user.Email,
                Nome = membro.Nome,
                Startup = startup != null ? startup.Nome : "Não definido",
                Cargo = membro.Cargo,
                Bio = membro.Bio
            };
            return View(perfil);
        }

        // Post: Usuarios/Perfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil(UsuarioPerfilViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var user = getUserLogedIn();
                var membro = user.Membro;
                if (membro != null)
                {
                    membro.Nome = usuario.Nome;
                    membro.Bio = usuario.Bio;
                    membro.Cargo = usuario.Cargo;
                    if (usuario.Foto != null && usuario.Foto.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(usuario.Foto.FileName);
                        filename = Path.Combine(Server.MapPath("/Content/images/avatar/"), filename);
                        membro.Foto = LerFoto(filename);
                    }

                    var db = new MovimentaContext();
                    db.Entry(membro).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.State = 1; //Sucesso na operação
                }
                var startup = membro.GetStartup();
                usuario = new UsuarioPerfilViewModel
                {
                    Email = user.Email,
                    Nome = membro.Nome,
                    Startup = startup != null ? startup.Nome : "Não definido",
                    Cargo = membro.Cargo,
                    Bio = membro.Bio
                };
            }
            return View(usuario);
        }

        public string LerFoto(String path)
        {
            var foto =
                System.IO.File.ReadAllBytes(path);
            return LerFoto(foto);
        }

        public string LerFoto(byte[] foto)
        {
            var base64 = Convert.ToBase64String(foto);
            return String.Format("data:image/jpg;base64,{0}", base64);
        }

        //GET: Usuarios/DadosCadastro
        public ActionResult DadosCadastro()
        {
            var dados = new DadosViewModel {Pais = ListaPaises()};
            return View(dados);
        }

        //GET: Usuarios/Projectos
        public ActionResult Projectos()
        {
           var membro = getUserLogedIn().Membro;
           var projectos = new ProjectoRepository().GetProjectos(membro);

            var model = projectos.Select(projecto => new ProjectosCriadosViewModel
            {
                Id = projecto.ProjectoId,
                Titulo = projecto.Titulo,
                Autor = membro.Nome,
                Estado = projecto.Estado,
                Arrecadado = projecto.Arrecadado,
                Meta = projecto.Meta,
                DiasRestantes = (projecto.DataFim - DateTime.Now)?.Days ?? 0
            }).ToList();

            return View(model);
        }

        //GET: Usuarios/Apoios
        public ActionResult Apoios()
        {
            var membro = getUserLogedIn().Membro;
            var projectos = new ProjectoRepository().GetProjectos(membro);

            var model = projectos.Select(projecto => new ProjectosCriadosViewModel
            {
                Id = projecto.ProjectoId,
                Titulo = projecto.Titulo,
                Autor = membro.Nome,
                Estado = projecto.Estado,
                Arrecadado = projecto.Arrecadado,
                Meta = projecto.Meta,
                DiasRestantes = (projecto.DataFim - DateTime.Now)?.Days ?? 0
            }).ToList();

            return View(model);
        }
        ApplicationUser getUserLogedIn()
        {
            var user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            return user;
        }

        public List<SelectListItem> ListaPaises()
        {
            List<SelectListItem> listapaises = new PaisRepository().GetPaises().Select(pais => new SelectListItem {Text = pais.Nome, Value = pais.IdPais.ToString()}).ToList();

            return listapaises;
        }

        public JsonResult ListaProvincias(string idPais)
        {
            Debug.WriteLine("pais: " + idPais);
            var provincias = new ProvinciaRepository().GetProvinciasPais(int.Parse(idPais));
            
            List<SelectListItem> listaProvincias =
                provincias.Select(
                    provincia => new SelectListItem {Text = provincia.Nome, Value = provincia.IdProvincia.ToString()})
                    .ToList();
            return Json(listaProvincias, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaMunicipios(string idProvincia)
        {
            var municipios = new MunicipioRepository().GetMunicipioProvincia(int.Parse(idProvincia));
            List<SelectListItem> listaMunicipio =
                municipios.Select(
                    municipio => new SelectListItem { Text = municipio.Nome, Value = municipio.IdMunicipio.ToString() })
                    .ToList();
            return Json(listaMunicipio, JsonRequestBehavior.AllowGet);
        }
    }
}