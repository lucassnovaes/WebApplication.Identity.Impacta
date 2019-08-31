using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impacta.Identity.Terminal
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Console.WriteLine("Digite seu usuário: ");
				var usuario = Console.ReadLine();
				Console.WriteLine("Digite sua senha: ");
				string senha = Console.ReadLine();


				var usuarioArmazenado = new UserStore<IdentityUser>();
				var usuarioGerenciador = new UserManager<IdentityUser>(usuarioArmazenado);
				var result = usuarioGerenciador.Create(new IdentityUser(usuario), senha);

				Console.WriteLine("Status create {0}", result.Succeeded);

				var userClaim = usuarioGerenciador.FindByName(usuario);
				usuarioGerenciador.AddClaim(userClaim.Id, new Claim("Nome_Usuario","Lucas"));
				//usuarioGerenciador.AddClaim(userClaim.Id, new Claim(ClaimTypes.Name,"Lucas"));
				var validaSenha = usuarioGerenciador.CheckPassword(userClaim, senha);
				Console.WriteLine("Senha verificada: {0}", validaSenha);
				Console.ReadLine();
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
		}
	}
}
