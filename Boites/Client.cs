using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
	internal class Client
	{
		public required long Numero { get; init; }
		public required string Nom { get; set; }
		public required string Prenom { get; set; }
		public required string Adresse { get; set; }	
	}
}
