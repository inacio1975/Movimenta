using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarrosselRepository carrosselRepository;
        private IProjectoRepository projectRepository;
        private ISucessoRepository sucessoRepository;

        public HomeController()
        {
            projectRepository = new ProjectoRepository();
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
            model.Projectos = projectRepository.GetProjectos().Take(3);
            model.Sucessos = sucessoRepository.GetSucessos();
            model.CampanhasPopulares = projectRepository.GetPopularProjects().Take(3);
            model.Categorias = new MovimentaContext().Categorias.ToList();
            model.Parceiros = DbInitializer.GetPartners();
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

        [HttpPost]
        public ActionResult Contact(MailViewModel model)
        {
            if (ModelState.IsValid)
            {
                //prepare email
                var fromAddress = model.email.ToLower();
                var subject = model.subject;
                var name = model.name;
                var message = new StringBuilder();
                message.Append("Nome: " + name + "\n");
                message.Append("Email: " + fromAddress + "\n\n");
                message.Append(model.message);

                //start email Thread
                var mail = new MailMovimenta(fromAddress, subject, message);
                var tEmail = new Thread(() => mail.Send());
                tEmail.Start();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SendMessage(MailViewModel model)
        {
            var fromAddress = model.email.ToLower();
            var subject = model.subject;
            var name = model.name;
            var message = new StringBuilder();
            message.Append("Nome: " + name + "\n");
            message.Append("Email: " + fromAddress + "\n\n");
            message.Append(model.message);

            //start email Thread
            var mail = new MailMovimenta(fromAddress, subject, message);
            var tEmail = new Thread(() => mail.Send());
            tEmail.Start();

            return Json("Sua mensagem foi enviada! Obrigado por entrar em contacto", JsonRequestBehavior.AllowGet);
        }

        public string LerFoto(string path)
        {
            var foto =
                System.IO.File.ReadAllBytes(HttpContext.Server.MapPath(path));
            return LerFoto(foto);
        }

        public string LerFoto(byte[] foto)
        {
            var base64 = Convert.ToBase64String(foto);
            return string.Format("data:image/jpg;base64,{0}", base64);
        }

        public ActionResult Equipe(int? id)
        {
            var dados = new[]
            {
                new TeamViewModel
                {
                    Id = 1,
                    Nome = "Ketson Lima Rodrigues",
                    Cargo = "C E O",
                    Telefone = "+53 55 351 887",
                    Email = "klimarodrigues@movimenta.ao",
                    Foto = LerFoto("/Content/images/avatar/team-single-info.jpg"),
                    Citacao =
                        "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 2,
                    Nome = "Osvaldo Tomé Quingongo",
                    Cargo = "Director de Projectos",
                    Telefone = "+53 58 871 448",
                    Email = "osvaldotq@movimenta.ao",
                    Foto = LerFoto("/Content/images/team/osvaldo.jpg"),
                    Citacao =
                        "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 3,
                    Nome = "Inácio Kawala",
                    Cargo = "Desenvolvedor - Full Stack",
                    Telefone = "+244 927 781 657",
                    Email = "inacio.kawala@movimenta.ao",
                    Foto = LerFoto("/Content/images/team/inacio.jpg"),
                    Citacao =
                        "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 4,
                    Nome = "Jandira Lourenço",
                    Cargo = "Comunity Manager",
                    Telefone = "+244 924 602 807",
                    Email = "lelialourenco@movimenta.ao",
                    Foto = LerFoto("/Content/images/team/lelia.jpg"),
                    Citacao =
                        "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 5,
                    Nome = "Bruno Yonng",
                    Cargo = "Gestor de tráfego",
                    Telefone = "+53 55 351 887",
                    Email = "byonng1994@movimenta.ao",
                    Foto = LerFoto("/Content/images/team/bruno.jpg"),
                    Citacao =
                        "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
                },
                new TeamViewModel
                {
                    Id = 6,
                    Nome = "Sandro",
                    Cargo = "Desginer",
                    Telefone = "+53 55 351 887",
                    Email = "byonng1994@movimenta.ao",
                    Webpage = "https://www.movimenta.ao",
                    Foto = LerFoto("/Content/images/team/sandro.jpg"),
                    Citacao =
                        "Os productos na Movimenta são únicos, muito interessantes. Não são como os que encontramos no mercado. São novas tecnologias que as companhias nem sbem que serão populares."
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