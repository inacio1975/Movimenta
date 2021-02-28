using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
    public class PagamentosController : Controller
    {
        private readonly MovimentaContext db;

        private ApplicationUser GetUserLogedIn()
        {
            var user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            return user;
        }

        // GET: Pagamentos
        public PagamentosController()
        {
            db = new MovimentaContext();

            try
            {
                ViewBag.Membro = GetUserLogedIn().Membro;
            }
            catch (NullReferenceException e)
            {
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult EscoherPagamento(int? id)
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
            var model = new EscolherValorViewModel
            {
                ProjectoId = projecto.ProjectoId,
                Projecto = projecto,
                Recompensas = projecto.GetRecompensas().ToList()
            };
            return View(model);
        }

        [HttpPost, ActionName("EscoherPagamento")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EscoherPagamento(EscolherValorViewModel model)
        {
            
            return RedirectToAction("DetalhePagamentos", model); 
        }

        public ActionResult DetalhePagamentos(double valor, int? recompensaId, int? projectoId, int? pagamentoSelecionado)
        {
            var model = new DetalhePagamentoViewModel
            {
                Valor = valor,
                Recompensa = db.Recompensas.Find(recompensaId),
                Projecto = db.Projectos.Find(projectoId),
                PagamentoSelecionado = pagamentoSelecionado??0
            };
            return View(model);
        }
    }
}