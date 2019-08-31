using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Impacta.Autenticacao.Mvc.Models
{
	public class Usuario : IUser
	{
		public string Id { get; set; }
		[Required(ErrorMessage ="Usuário obrigatório")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Senha obrigatório")]
		[MaxLength(8,ErrorMessage = "Quantidade maxima permitida: 8")]
		public string Password { get; set; }
	}
}