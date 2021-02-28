using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class ContaController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ContaController()
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

        public ContaController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;

            try
            {
                ViewBag.Membro = GetUserLogedIn().Membro;
            }
            catch (NullReferenceException e)
            {

            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        //
        // GET: /Conta/Entrar
        [AllowAnonymous]
        public ActionResult Entrar(string returnUrl)
        {
            ViewBag.active = 1;
            ViewBag.Title = "Entrar";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Conta/Entrar
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Entrar(EntrarViewModel model, string returnUrl)
        {
            ViewBag.active = 1;
            ViewBag.Title = "Entrar post";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, shouldLockout: false);
            
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Usuario ou senha incorrecta");
                    return View(model);
            }
        }

        //
        // GET: /Conta/Registar
        [AllowAnonymous]
        public ActionResult Registrar()
        {
            ViewBag.Title = "Registrar";
            ViewBag.active = 2;
            return View();
        }

        //
        // GET: /Conta/Registar
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registrar(RegistrarViewModel usuario)
        {
            ViewBag.Title = "Registrar";
            if (ModelState.IsValid)
            {
                var defaultAvatarfn = HttpContext.Server.MapPath("/Content/images/avatar/default_avatar.png");
                var defaultAvatar = System.IO.File.ReadAllBytes(defaultAvatarfn);
                var user = new ApplicationUser
                {
                    UserName = usuario.Email,
                    Email = usuario.Email,
                    Membro = new Membro
                    {
                        Nome = usuario.Email,
                        Foto = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(defaultAvatar))
                    }
                };
                var result = await UserManager.CreateAsync(user, usuario.Password);
                if (result.Succeeded)
                {
                    //new MembroUsuarioRepository().AddElement(new MembroUsuario());
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmarEmail", "Conta", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(user.Id, "Movimenta: Confirme a sua conta", "Por favor, confirme seu endereço de e-mail clicando <a href=\"" + callbackUrl + "\">aqui</a>");
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(usuario);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Conta/EsqueciSenha
        [AllowAnonymous]
        public ActionResult EsqueciSenha()
        {
            ViewBag.Title = "Recuperar Senha";
            return View();
        }

        //
        // GET: /Conta/ConfirmarEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmarEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmarEmail" : "Error");
        }

        //
        // GET: /Conta/EsqueciSenha
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EsqueciSenha(EsqueciChaveViewModel recuperasenha)
        {
            ViewBag.Title = "Recuperar Senha";
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(recuperasenha.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ConfirmacaoEsquecimentoSenha");
                }
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Conta", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Movimenta: Configurar nova Password", "Por favor configure uma nova palavra passe clicando <a href=\"" + callbackUrl + "\">Aqui</a>");
                return RedirectToAction("ConfirmacaoEsquecimentoSenha", "Conta");

            }
            // If we got this far, something failed, redisplay form
            return View(recuperasenha);
        }

        //
        // GET: /Conta/ConfirmacaoEsquecimentoSenha
        [AllowAnonymous]
        public ActionResult ConfirmacaoEsquecimentoSenha()
        {
            return View();
        }

        //
        // GET: /Conta/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Conta/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ConfirmacaoEsquecimentoSenha", "Conta");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ConfirmacaoEsquecimentoSenha", "Conta");
            }
            AddErrors(result);
            return View();
        }
    }
}