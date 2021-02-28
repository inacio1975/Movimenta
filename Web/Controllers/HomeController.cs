using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarrosselRepository carrosselRepository;
        private IProjectoRepository projectoRepository;
        private ISucessoRepository sucessoRepository;

        public HomeController()
        {
            projectoRepository = new ProjectoRepository();
            carrosselRepository = new CarrosselRepository();
            sucessoRepository = new SucessoRepository();

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

        public ActionResult Index()
        {
            ViewBag.slides = carrosselRepository.GetCarrossels();
            var model = new HomeViewModel();
            model.Projectos = new MovimentaContext().Projectos.ToList();
            model.Sucessos = new SucessoRepository().GetSucessos();
            model.Categorias = new MovimentaContext().Categorias.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public string LerFoto(String path)
        {
            var foto =
                System.IO.File.ReadAllBytes(HttpContext.Server.MapPath(path));
            return LerFoto(foto);
        }

        public string LerFoto(byte[] foto)
        {
            var base64 = Convert.ToBase64String(foto);
            return String.Format("data:image/jpg;base64,{0}", base64);
        }

        public ActionResult Equipe(int? id)
        {
            var dados = new[]
            {
                new TeamViewModel
                {
                    Id = 1,
                    Nome = "Ketson Lima Rodrigues",
                    Cargo = "C E O, Founder",
                    Telefone = "+53 55 351 887",
                    Email = "ketsonlimarodrigues@movimenta.ao",
                    Foto = LerFoto("/Content/images/avatar/team-single-info.jpg"),
                    Citacao = "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 2,
                    Nome = "Osvaldo Tomé Quingongo",
                    Cargo = "C O O, Co-Founder",
                    Telefone = "+53 58 871 448",
                    Email = "osvaldotq@movimenta.ao",
                    Foto = LerFoto("/Content/images/avatar/team_osvaldo.jpg"),
                    Citacao = "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 3,
                    Nome = "Inácio Kawala",
                    Cargo = "C T O, CO-Founder",
                    Telefone = "+244 927 781 657",
                    Email = "inacio.kawala@movimenta.ao",
                    Foto = LerFoto("/Content/images/avatar/team_inacio.jpg"),
                    Citacao = "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 4,
                    Nome = "Onilda Patricia",
                    Cargo = "Acessora Jurídica",
                    Telefone = "+53 55 351 887",
                    Email = "onilda@movimenta.ao",
                    Foto = LerFoto("/Content/images/avatar/avatar_5.jpg"),
                    Citacao = "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 5,
                    Nome = "Usuario de teste",
                    Cargo = "C E O, Founder",
                    Telefone = "+53 55 351 887",
                    Email = "ketsonlimarodrigues@movimenta.ao",
                    Foto = LerFoto("/Content/images/avatar/team-single-info.jpg"),
                    Citacao = "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                }
            }.ToList();
            var ida = 1;
            if (id != null)
                ida = (int) id;
            ViewBag.Team = dados.Find(m => m.Id == ida);
            dados.Remove(ViewBag.Team);
            return View(dados);
        }

        public ActionResult PerguntasFrequentes()
        {
            return View();
        }
    }
}