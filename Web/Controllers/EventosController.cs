using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
using Domain.Entities;
using Web.Models;

namespace Web.Controllers
{
    public class EventosController : Controller
    {
        // GET: Eventos
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Eliminar(int? idevento)
        {
            if (idevento == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var eventoRepository = new EventoRepository();

            eventoRepository.Eliminar(idevento.Value);
            return RedirectToAction("Eventos","Dashboard");
        }

        [HttpPost]
        public ActionResult Editar(EventoViewModel evento)
        {
            bool flag = false;
            var eventosRepository = new EventoRepository();
            if (evento?.EventoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var eventoid = evento.EventoId;
            Debug.WriteLine("Actualizado: " + evento.EventoId);
            if (eventosRepository.Exists(evento.EventoId))
            {
                var eventoNovo = new Evento
                {
                    EventoId = evento.EventoId,
                    Titulo = evento.Titulo,
                    DescricaoCurta = evento.DescricaoCurta,
                    DescricaoLarga = evento.DescricaoLarga,
                    Local = evento.Local,
                    Data = evento.Data,
                    FotoUrl = evento.FotoUrl
                };
                eventoNovo.Titulo = evento.Titulo;
                flag = eventosRepository.Editar(eventoNovo);
                Debug.WriteLine("Actualizado");
            }
            if (flag == true)
            {
                TempData["sucesso"] = "Elemento Editado correctamente";
                Debug.WriteLine("sucesso");
            }
            else
            {
                TempData["erro"] = "Vão foi possivel actualizar a  informaçao pretendida";
                Debug.WriteLine("erro");
            }

            return RedirectToAction("Eventos", "Dashboard");
        }

        public JsonResult Evento(string id)
        {
            
            var evento = new EventoRepository().GetEvento(int.Parse(id));
            return Json(evento, JsonRequestBehavior.AllowGet);
        }
    }
}