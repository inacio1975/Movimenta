using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "adm")]
    public class DashboardController : Controller
    {
        private readonly MovimentaContext db = new MovimentaContext();
        private readonly ApplicationDbContext security = new ApplicationDbContext();

        public DashboardController()
        {
            try
            {
                ViewBag.Membro = GetUserLogedIn().Membro;
            }
            catch (NullReferenceException e)
            {
            }
        }

        private ApplicationUser GetUserLogedIn()
        {
            var user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            return user;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        //GET: Dashboard
        public ActionResult Projectos()
        {
            var projectRepository = new ProjectoRepository();
            var projectos = projectRepository.ListaProjectosGestao();
            var model = projectos.Select(projecto => new ProjectosCriadosViewModel
            {
                Id = projecto.ProjectoId,
                Titulo = projecto.Titulo,
                Autor = db.Membros.Find(projecto.MembroId).Nome,
                Estado = projecto.Estado,
                Arrecadado = projecto.Arrecadado,
                Meta = projecto.Meta,
                DiasRestantes = (projecto.DataFim - DateTime.Now)?.Days ?? 0
            }).ToList();
            return View(model);
        }

        public ActionResult DetalheProjecto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projecto = db.Projectos.Find(id);
            if (projecto == null)
            {
                return HttpNotFound();
            }
            var model = new ProjectoViewModel
            {
                ProjectoId = projecto.ProjectoId,
                Titulo = projecto.Titulo
            };
            return View(model);
        }

        // GET: Dashboard
        public ActionResult Adicionar(string q)
        {
            return View();
        }

        // GET: Dashboard/Carrossel
        public ActionResult Carrossel()
        {
            var carrossels = db.Carrossels.ToList();
            return View(carrossels);
        }

        // GET: Dashboard/Seguranca
        public ActionResult Seguranca()
        {
            var usermanager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var usuarios = usermanager.Users.ToList();

            SegurancaViewModel model = new SegurancaViewModel
            {
                Usuarios =  usuarios.Select(usuario => new UsuarioViewModel
                {
                    ID = usuario.Id,
                    Nome = usuario.Membro.Nome,
                    Email = usuario.Email,
                    Organizacao = usuario.Membro.GetStartup().Nome,
                    Foto = usuario.Membro.Foto,
                    Roles = usuario.GetRoles()
                }).ToList()
        };
            return View(model);
        }

        // GET: Dashboard/Categorias
        public ActionResult Categorias()
        {
            TempData["erro"] = null;
            TempData["sucesso"] = null;
            var categorias = db.Categorias.ToList();
            var model = categorias.Select(categoria => new CategoriaViewModel
            {
                Id = categoria.CategoriaId,
                Nome = categoria.nome,
                NumProjectos = new ProjectoRepository().GetProjectosByCategory(categoria.CategoriaId).Count
            }).OrderByDescending(categoria => categoria.NumProjectos).ToList();
            return View(model);
        }

        // GET: Dashboard/Eventos
        public ActionResult Eventos()
        {
            var eventos = new EventoRepository().GetEventos();
            var model = eventos.Select(evento => new EventoViewModel
            {
                EventoId = evento.EventoId,
                Titulo = evento.Titulo,
                FotoUrl = evento.FotoUrl,
                DescricaoCurta = evento.DescricaoCurta,
                DescricaoLarga = evento.DescricaoLarga,
                Local = evento.Local,
                Data = evento.Data ?? DateTime.Now
            });
            return View(model);
        }

        // GET: Dashboard/Sucessos
        public ActionResult Sucessos()
        {
            return View();
        }

        // GET: Dashboard/Parceiros
        public ActionResult Parceiros()
        {
            return View();
        }

        public ActionResult AdicionarEvento()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult AdicionarEvento(string tituloEvento, string shortdesc, string longdesc, string local, DateTime data, string fotourl="")
        {
            var model = new Evento
            {
                Titulo = tituloEvento,
                DescricaoCurta = shortdesc,
                DescricaoLarga = longdesc,
                Local = local,
                Data = data,
                FotoUrl = fotourl
            };
            if(new EventoRepository().Add(model))
            {

            }
            else
            {
                
            }
            return RedirectToAction("Eventos");
        }

        [HttpPost]
        public ActionResult EliminarEvento(int? idevento)
        {
            if(idevento == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var eventoRepository = new EventoRepository();
            
                eventoRepository.Eliminar(idevento.Value);
            return RedirectToAction("Eventos");
        }

        public ActionResult OcultarCampanha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projecto = db.Projectos.Find(id);
            if (projecto == null)
            {
                return HttpNotFound();
            }
            if (projecto.Estado != Estado.Publicado) return View();
            projecto.Estado = Estado.Ocultado;
            db.Entry(projecto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Projectos");
        }

       

        public ActionResult PublicarCampanha(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projecto = db.Projectos.Find(id);
            if (projecto == null)
            {
                return HttpNotFound();
            }

            if (projecto.Estado != Estado.Analise) return View();
            projecto.DataInicio = DateTime.Now;
            projecto.DataFim = projecto.DataInicio.Value.Add(new TimeSpan(projecto.Duracao.Value,0,0,0));
            projecto.Estado = Estado.Publicado;
            db.Entry(projecto).State = EntityState.Modified;
            db.SaveChanges();
            var email = db.Membros.Find(projecto.MembroId).Nome;
            var msg = new StringBuilder();
            msg.Append("Caro " + db.Membros.Find(projecto.MembroId) + ", \n");
            msg.Append("Somos de informar que a sua campanha foi ocultada da lista das campanhas da nossa plataforma.\n");
            msg.Append("\nPara mais informações, por favor, contacte à nossa equipe respondendo a este email\n");
            msg.Append("Atenciosamente, \n");
            msg.Append("A equipe Movimenta\n");
            var mail = new MailMovimenta(email,"Actualização de estado",msg);
            mail.Send();

            return RedirectToAction("Projectos");
        }

        public ActionResult TornarVisivel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projecto = db.Projectos.Find(id);
            if (projecto == null)
            {
                return HttpNotFound();
            }

            if (projecto.Estado != Estado.Ocultado) return RedirectToAction("Projectos");
           
            projecto.Estado = Estado.Publicado;
            db.Entry(projecto).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Projectos");
        }

        public ActionResult DevolverCampanha(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projecto = db.Projectos.Find(id);
            if (projecto == null)
            {
                return HttpNotFound();
            }

            if (projecto.Estado != Estado.Analise) return RedirectToAction("Projectos");
            projecto.Estado = Estado.Rascunho;
            db.Entry(projecto).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Projectos");
        }
    }
}