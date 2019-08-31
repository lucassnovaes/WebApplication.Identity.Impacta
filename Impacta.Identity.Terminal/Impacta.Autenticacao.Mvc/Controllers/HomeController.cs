using Impacta.Autenticacao.Mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Impacta.Autenticacao.Mvc.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult AreaLivre()
		{
			ViewBag.Message = "Area Livre";

			return View();
		}

		[Authorize]
		public ActionResult AreaRestrita()
		{
			ViewBag.Message = "Area restrita";

			return View();
		}

		public ActionResult Login()
		{
			ViewBag.Message = "Login";

			return View();
		}
		[HttpPost]
		public ActionResult Login(Usuario usuario)
		{
			if (AuteticarUsuario(usuario))
			{
				return View("AreaRestrita");
			}
			else
			{
				return View("Index");
			}
		}

		private bool AuteticarUsuario(Usuario user)
		{
			
			string nome = user.UserName;
			string senha = user.Password;
			var usuarioStore = new UserStore<IdentityUser>();
			var usuarioGerenciador =
			new UserManager<IdentityUser>(usuarioStore);
			var usuario = usuarioGerenciador.Find(nome, senha);
			if (usuario != null)
			{
				var gerenciadorDeAutenticacao = HttpContext.GetOwinContext().Authentication;
				var identidade = usuarioGerenciador.CreateIdentity(usuario,
					DefaultAuthenticationTypes.ApplicationCookie);
				gerenciadorDeAutenticacao.SignIn(new AuthenticationProperties(){ IsPersistent = false },identidade);
				return true;
			}
			else
			{
				return false;
			}
		}

		public ActionResult CriarLogin()
		{
			Usuario usuario = new Usuario(); 

			return View(usuario);
		}

		[HttpPost]
		public ActionResult CriarLogin(Usuario usuario)
		{
			if (SalvarUsuario(usuario))
			{
				return View("Index");
			}
			else
			{
				return View("CriarLogin");
			}
			
		}

		private bool SalvarUsuario(Usuario usuario)
		{
			bool retorno = false;
			string nome = usuario.UserName;
			string senha = usuario.Password;
			var usuarioStore = new UserStore<IdentityUser>();
			var usuarioGerenciador =
			new UserManager<IdentityUser>(usuarioStore);
			var usuarioInfo = new IdentityUser() { UserName = usuario.UserName };
			IdentityResult resultado = usuarioGerenciador.Create(usuarioInfo, usuario.Password);
			if (resultado.Succeeded)
			{
				var gerenciadorDeAutenticacao = HttpContext.GetOwinContext().Authentication;
				var identidadeUsuario = usuarioGerenciador.CreateIdentity( usuarioInfo, 
					DefaultAuthenticationTypes.ApplicationCookie);
				gerenciadorDeAutenticacao.SignIn(new AuthenticationProperties() { },identidadeUsuario);
				retorno = true;
			}
			else 
			{
				retorno = false;
				ViewBag.Erro = resultado.Errors;
			}
			return retorno;
		}
	}
}