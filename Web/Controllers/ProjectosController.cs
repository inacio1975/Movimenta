using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
    public class ProjectosController : Controller
    {
        private readonly MovimentaContext db = new MovimentaContext();
        private readonly IProjectoRepository projectoRepository;

        public ProjectosController(IProjectoRepository repository)
        {
            projectoRepository = repository;
            //trabalhosRepository = trabalhos;

            try
            {
                ViewBag.Membro = GetUserLogedIn().Membro;
            }
            catch (NullReferenceException e)
            {
                System.Console.WriteLine(e.Message);
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

        // GET: Projectos
        public ActionResult Index(int? page)
        {
            var intpage = 1;
            if (page != null) intpage = page.Value;

            Debug.WriteLine("page: " + intpage);
            var projectos = projectoRepository.GetProjectosWithPage(intpage, 6).ToList();
            var model = new CampanhaViewModel
            {
                Projectos = projectos,
                paginator = new CampanhaViewModel.Paginator(intpage, projectoRepository.GetProjectos())
            };
            return View(model);
        }

        // GET: Projectos/Detalhe/5
        public ActionResult Detalhe(int? id)
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

            var model = new DetalheViewModel
            {
                ProjectoId = projecto.ProjectoId,
                Titulo = projecto.Titulo,
                Keywords = projecto.Keywords,
                FotoUrl = projecto.Foto,
                Video = projecto.Video,
                DescricaoCurta = projecto.Descricao,
                SobreCampanha = projecto.Sobre,
                UsoFundo = projecto.Objectivofundo,
                Progresso = projecto.GetProgresso(),
                Arrecadado = projecto.Arrecadado ?? 0,
                Meta = projecto.Meta ?? 0,
                DataFim = projecto.DataFim ?? DateTime.Today,
                DataInicio = projecto.DataInicio ?? DateTime.Today,
                Novidades = db.Novidades.Where(nov => nov.ProjectoId == projecto.ProjectoId),
                Comentarios = db.Comentarios.Where(com => com.ProjectoId == projecto.ProjectoId).OrderByDescending(com => com.Data).Take(7),
                Movimentadores = db.Movimentadores.Where(mov => mov.ProjectoId == projecto.ProjectoId),
                Recompensas = db.Recompensas.Where(rec => rec.ProjectoId == projecto.ProjectoId),
                Autor = db.Membros.Find(projecto.MembroId),
                Projectos = new MovimentaContext().Projectos.Take(3)
            };
            model.Projectos = new MovimentaContext().Projectos.ToList().Take(3);
            Debug.WriteLine("Debug detalhe: " + model.Projectos.Count());
            
            return View(model);
        }

        // GET: Projectos/Criar
        [Authorize]
        public ActionResult Criar()
        {
            var model = new CriarViewModel();
            var campanhas =
                new MovimentaContext().Campanhas.Select(
                    campanha => new SelectListItem {Text = campanha.Nome, Value = campanha.CapanhaId.ToString()})
                    .ToList();
            model.Campanha = new SelectList(campanhas, "Value", "Text", model.CampanhaSelecionada);
            return View(model);
        }

        // POST: Projectos/Criar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Criar")]
        [ValidateAntiForgeryToken]
        [Authorize]
        //public ActionResult Criar([Bind(Include = "ProjectoId,MembroId,Titulo,Foto,Descricao,Sobre,Objectivofundo,Campanha,Meta,Arrecadado,DataInicio,DataFim")] Projecto projecto)
        public ActionResult Criar(CriarViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                db.Enderecos.Add(new Endereco {IdPais = 1,IdProvincia = 11,IdMunicipio = 95,Rua = "Coloque a rua"});
                db.SaveChanges();
                var endereco = new EnderecoCampanha {EnderecoId = db.Enderecos.ToList().Last().EnderecoId};
                db.EnderecoCampanhas.Add(endereco);
                db.SaveChanges();

                var projecto = new Projecto
                {
                    Titulo = model.Titulo,
                    MembroId = getUserLogedIn().MembroId,
                    Estado = Estado.Rascunho,
                    IdLocalizacao = endereco.IdLocalizacao
                };
                projectoRepository.AddProjecto(projecto);
                db.SaveChanges();

                //db.Projectos.Add(projecto);
                //db.SaveChanges();
                return RedirectToAction("Editar",
                    new {Id = projectoRepository.GetProjectos(getUserLogedIn().MembroId).ProjectoId});
            }
            return View(model);
        }

        private ApplicationUser getUserLogedIn()
        {
            var user =
                System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            return user;
        }

        // GET: Projectos/Editar
        [Authorize]
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var idprojecto = (int) id;
            var projecto = projectoRepository.GetProjectos(idprojecto);
            if (projecto == null)
            {
                return HttpNotFound();
            }

            var enderecocampanha = projecto.GetEnderecoCampanha();

            var model = new EditarViewModel
            {
                Titulo = projecto.Titulo,
                Foto = projecto.Foto,
                Video = projecto.Video,
                Descricao = projecto.Descricao,
                Sobre = projecto.Sobre,
                Objectivofundo = projecto.Objectivofundo,
                //Local = projecto.EnderecoCampanha,
                Keywords = projecto.Keywords,
                DiasArrecadacao = projecto.Duracao??0,
                Meta = projecto.Meta,
                CampanhaSelecionada = projecto.CapanhaId.ToString(),
                CategoriaSelecionada = projecto.CategoriaId.ToString(),
                Estado = projecto.Estado,
                PaisSelecionado = enderecocampanha.IdPais.ToString(),
                ProvinciaSelecionada = enderecocampanha.IdProvincia.ToString(),
                MunicipioSelecionado = enderecocampanha.IdMunicipio.ToString(),
                Rua = enderecocampanha.Rua
            };

            var categorias =
                db.Categorias.Select(
                    categoria => new SelectListItem {Text = categoria.nome, Value = categoria.CategoriaId.ToString()})
                    .ToList();
            model.Categoria = new SelectList(categorias, "Value", "Text", model.CategoriaSelecionada);

            var campanhas =
                db.Campanhas.Select(
                    campanha => new SelectListItem {Text = campanha.Nome, Value = campanha.CapanhaId.ToString()})
                    .ToList();
            model.Campanha = new SelectList(campanhas, "Value", "Text", model.CampanhaSelecionada);

            var paises = db.Paises.Select(pais => new SelectListItem {Text = pais.Nome, Value = pais.IdPais.ToString()});
            model.Pais = new SelectList(paises, "Value", "Text", model.PaisSelecionado);

            var provincias =
                db.Provincias.Select(
                    provincia => new SelectListItem {Text = provincia.Nome, Value = provincia.IdProvincia.ToString()});
            model.Provincia = new SelectList(provincias, "Value", "Text", model.ProvinciaSelecionada);

            var municipios =
                db.Municipios.Select(
                    municipio => new SelectListItem {Text = municipio.Nome, Value = municipio.IdMunicipio.ToString()});
            model.Municipio = new SelectList(municipios, "Value", "Text", model.MunicipioSelecionado);

            var recompensas = db.Recompensas.Where(rec => rec.ProjectoId == id).ToList();
            model.Recompensas = recompensas.Select(recompensa => new RecompensaViewModel
            {
                Id = recompensa.IdRecompensa,
                Item = recompensa.Item,
                RecDescricao = recompensa.Descricao,
                Quantidade = recompensa.Quantidade,
                ValorDoado = recompensa.ValorDoado,
                PrazoEntregaEstimado = recompensa.PrazoEntregaEstimado,
                LugarEntrega = recompensa.LugarEntrega
            }).ToList();

            return View(model);
        }


        // POST: Projectos/Editar
        [HttpPost, ActionName("Editar")]
        [ValidateAntiForgeryToken]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Editar(int? id, EditarViewModel model)
        {
            var categorias =
                db.Categorias.Select(
                    categoria => new SelectListItem {Text = categoria.nome, Value = categoria.CategoriaId.ToString()})
                    .ToList();

            var campanhas =
                db.Campanhas.Select(
                    campanha => new SelectListItem {Text = campanha.Nome, Value = campanha.CapanhaId.ToString()})
                    .ToList();

            var paises = db.Paises.Select(pais => new SelectListItem {Text = pais.Nome, Value = pais.IdPais.ToString()});

            var provincias =
                db.Provincias.Select(
                    provincia => new SelectListItem {Text = provincia.Nome, Value = provincia.IdProvincia.ToString()});

            var municipios =
                db.Municipios.Select(
                    municipio => new SelectListItem {Text = municipio.Nome, Value = municipio.IdMunicipio.ToString()});

            //if (ModelState.IsValid)
            //{
            if (id != null)
            {
                var idprojecto = (int) id;
                var projecto = projectoRepository.GetProjectos(idprojecto);
                if (projecto != null)
                {
                    if (model.ficherofoto != null && model.ficherofoto.ContentLength > 0)
                    {
                        var filename = Path.GetFileName(model.ficherofoto.FileName);
                        var filenameToSave = projecto.MembroId + "-" + projecto.ProjectoId + "-" + filename;
                        var path = Path.Combine(Server.MapPath("~/Content/images/campaigns"), filenameToSave);

                        //borrando el fichero antiguo
                        if (!projecto.Foto.IsEmpty())
                        {
                            var oldfile = Path.Combine(Server.MapPath("~/Content/images/campaigns"), projecto.Foto);
                            if (System.IO.File.Exists(oldfile))
                                System.IO.File.Delete(oldfile);
                        }

                        projecto.Foto = filenameToSave;
                        model.ficherofoto.SaveAs(path);
                    }

                    projecto.Titulo = !model.Titulo.IsEmpty() ? model.Titulo : projecto.Titulo;
                    projecto.Video = !model.Video.IsEmpty() ? model.Video : projecto.Video;
                    projecto.Descricao = !model.Descricao.IsEmpty() ? model.Descricao : projecto.Descricao;
                    projecto.Sobre = !model.Sobre.IsEmpty() ? model.Sobre : projecto.Sobre;
                    projecto.Objectivofundo = !model.Objectivofundo.IsEmpty()
                        ? model.Objectivofundo
                        : projecto.Objectivofundo;
                    projecto.Keywords = !model.Keywords.IsEmpty() ? model.Keywords : projecto.Keywords;
                    projecto.Descricao = !model.Descricao.IsEmpty() ? model.Descricao : projecto.Descricao;
                    projecto.Meta = !model.Meta.ToString().IsEmpty() ? model.Meta : projecto.Meta;
                    projecto.Duracao = !model.DiasArrecadacao.ToString().IsEmpty()
                        ? model.DiasArrecadacao
                        : projecto.Duracao;
                    projecto.CapanhaId = !model.CampanhaSelecionada.IsEmpty()
                        ? Convert.ToInt32(model.CampanhaSelecionada)
                        : projecto.CapanhaId;
                    projecto.CategoriaId = !model.CategoriaSelecionada.IsEmpty()
                        ? Convert.ToInt32(model.CategoriaSelecionada)
                        : projecto.CategoriaId;

                    var endereco = db.Enderecos.Find(projecto.IdLocalizacao);

                    var novoendereco = new Endereco
                    {
                        IdPais =
                            !model.PaisSelecionado.IsEmpty() ? Convert.ToInt32(model.PaisSelecionado) : endereco.IdPais,
                        IdProvincia =
                            !model.ProvinciaSelecionada.IsEmpty()
                                ? Convert.ToInt32(model.ProvinciaSelecionada)
                                : endereco.IdProvincia,
                        IdMunicipio =
                            !model.MunicipioSelecionado.IsEmpty()
                                ? Convert.ToInt32(model.MunicipioSelecionado)
                                : endereco.IdMunicipio,
                        Rua = !model.Rua.IsEmpty() ? model.Rua : endereco.Rua
                    };

                    endereco.IdPais = novoendereco.IdPais;
                    endereco.IdProvincia = novoendereco.IdProvincia;
                    endereco.IdMunicipio = novoendereco.IdMunicipio;
                    endereco.Rua = novoendereco.Rua;

                    db.Entry(projecto).State = EntityState.Modified;
                    db.SaveChanges();

                    db.Entry(endereco).State = EntityState.Modified;
                    db.SaveChanges();


                    if (!model.Item.IsEmpty())
                    {
                        var recompensa = new Recompensa
                        {
                            Item = model.Item,
                            Descricao = model.RecDescricao,
                            Quantidade = model.Quantidade,
                            ValorDoado = model.ValorDoado,
                            PrazoEntregaEstimado = model.PrazoEntregaEstimado,
                            LugarEntrega = model.LugarEntrega,
                            ProjectoId = projecto.ProjectoId
                        };

                        db.Recompensas.Add(recompensa);
                        db.SaveChanges();
                    }

                    ViewBag.State = 1;

                    var model1 = new EditarViewModel
                    {
                        Titulo = projecto.Titulo,
                        Foto = projecto.Foto,
                        Video = projecto.Video,
                        Descricao = projecto.Descricao,
                        Sobre = projecto.Sobre,
                        Objectivofundo = projecto.Objectivofundo,
                        Keywords = projecto.Keywords,
                        Meta = projecto.Meta,
                        CampanhaSelecionada = projecto.CapanhaId.ToString(),
                        CategoriaSelecionada = projecto.CategoriaId.ToString(),
                        Estado = projecto.Estado,
                        PaisSelecionado = projecto.GetEnderecoCampanha().IdPais.ToString(),
                        ProvinciaSelecionada = projecto.GetEnderecoCampanha().IdProvincia.ToString(),
                        MunicipioSelecionado = projecto.GetEnderecoCampanha().IdMunicipio.ToString(),
                        Rua = projecto.GetEnderecoCampanha().Rua
                    };
                    model1.Categoria = new SelectList(categorias, "Value", "Text", model.CategoriaSelecionada);
                    model1.Campanha = new SelectList(campanhas, "Value", "Text", model.CampanhaSelecionada);
                    model1.Pais = new SelectList(paises, "Value", "Text", model.PaisSelecionado);
                    model1.Provincia = new SelectList(provincias, "Value", "Text", model.ProvinciaSelecionada);
                    model1.Municipio = new SelectList(municipios, "Value", "Text", model.MunicipioSelecionado);
                    RedirectToAction("Editar", id);
                    return View(model1);
                }
            }
            //}

            model.Categoria = new SelectList(categorias, "Value", "Text", model.CategoriaSelecionada);
            model.Campanha = new SelectList(campanhas, "Value", "Text", model.CampanhaSelecionada);
            model.Pais = new SelectList(paises, "Value", "Text", model.PaisSelecionado);
            model.Provincia = new SelectList(provincias, "Value", "Text", model.ProvinciaSelecionada);
            model.Municipio = new SelectList(municipios, "Value", "Text", model.MunicipioSelecionado);
            ViewBag.State = 0;
            return View("Editar", model);
        }

        public ActionResult EliminarRecompensa(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var idrecompensa = (int)id;
            var recompensa = db.Recompensas.Find(idrecompensa);
            if (recompensa == null)
            {
                return HttpNotFound();
            }

            db.Recompensas.Remove(recompensa);
            db.SaveChanges();

            RedirectToAction("Editar");
            
            return View("Editar");
        }

        // GET: Projectos/Apoios
        [Authorize]
        public ActionResult Apoios()
        {
            var projectos = projectoRepository.GetProjectos().ToList();
            return View();
        }

        // GET: Projectos/EviarRevisao
        [Authorize]
        public ActionResult EviarRevisao(int id)
        {
            return View();
        }

        // GET: Projectos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
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
            return View(projecto);
        }

        // POST: Projectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            var projecto = db.Projectos.Find(id);
            db.Projectos.Remove(projecto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public JsonResult Comentar(ComentarioViewModel model)
        {
            var projecto = db.Projectos.Find(model.ProjectoId);
            var usuario = db.Membros.Find(getUserLogedIn().MembroId);

            if (projecto != null && usuario != null)
            {
                var comentario = new Comentario
                {
                    comentario = model.Comentario,
                    Data = DateTime.Now,
                    MembroId = usuario.MembroId,
                    ProjectoId = projecto.ProjectoId
                };

                Debug.WriteLine("Antes de salvar " + model.Comentario);

                db.Comentarios.Add(comentario);
                db.SaveChanges();

                Debug.WriteLine("depois de salvar" + model.Comentario);

                /*var comentarios = db.Comentarios.Select(coment => new ComentarioViewModel
                {
                    Nome = usuario.Nome,
                    Data = coment.Data.ToString("dd 'de' MMMM 'de' yyyy"),
                    Foto = usuario.Foto,
                    Comentario = coment.comentario
                }).Where(coment => coment.ProjectoId == projecto.ProjectoId).Distinct();*/

                return Json("teste de coment+ario", JsonRequestBehavior.AllowGet);
            }
            
            return Json("Resultado desde controlador", JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> ListaPaises()
        {
            var listapaises =
                new PaisRepository().GetPaises()
                    .Select(pais => new SelectListItem {Text = pais.Nome, Value = pais.IdPais.ToString()})
                    .ToList();
            return listapaises;
        }

        public JsonResult ListaProvincias(string idPais)
        {
            var provincias = new ProvinciaRepository().GetProvinciasPais(int.Parse(idPais));
            var listaProvincias =
                provincias.Select(
                    provincia => new SelectListItem {Text = provincia.Nome, Value = provincia.IdProvincia.ToString()})
                    .ToList();
            return Json(listaProvincias, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaMunicipios(string idProvincia)
        {
            var municipios = new MunicipioRepository().GetMunicipioProvincia(int.Parse(idProvincia));
            var listaMunicipio =
                municipios.Select(
                    municipio => new SelectListItem {Text = municipio.Nome, Value = municipio.IdMunicipio.ToString()})
                    .ToList();
            return Json(listaMunicipio, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}