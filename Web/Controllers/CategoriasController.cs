using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
using Domain.Entities;

namespace Web.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dashboard
        [HttpPost]
        public ActionResult Adicionar(string q)
        {
            if(q == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var categoria = new Categoria {nome = q};
            if (new CategoriaRepository().Adicionar(categoria))
            {
                TempData["sucesso"] = "Elemento adicionado";
            }
            else
            {
                TempData["erro"] = "Erro ao adicionar a categoria";
            }

            return RedirectToAction("Categorias", "Dashboard");
        }

        [HttpPost]
        public ActionResult Editar(int? idcategoria, string q)
        {
            
            bool flag = false;
            if (idcategoria == null || q == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoriaid = idcategoria.Value;
            var categoria = new MovimentaContext().Categorias.Find(categoriaid);
            if (categoria != null)
            {
                categoria.nome = q;
                flag = new CategoriaRepository().Editar(categoria);
            }
            if(flag)
            {
                TempData["sucesso"] = "Elemento Editado correctamente";
            }
            else
            {
                TempData["erro"] = "Vão foi possivel actualizar a  informaçao pretendida";
            }

            return RedirectToAction("Categorias", "Dashboard");
        }

        public ActionResult Eliminar(int id)
        {
            try
            {
                var result = new CategoriaRepository().Eliminar(id);
                if (result)
                    TempData["sucesso"] = "Elemento eliminado";
                else
                    TempData["erro"] = "Não foi possivel eliminar o elemento";

            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Categorias", "Dashboard");
        }

    }
}