using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impacta.Identity.Terminal
{
	public class Usuario : IUser
	{
		public string Id { get; set; }
		public string UserName { get; set; }
	}
}
