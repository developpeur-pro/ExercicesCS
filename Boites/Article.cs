using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
	internal class Article
	{
		public string Libelle { get; }
		public double Volume { get; }

		public Article(string libelle, double volume)
		{
			Libelle = libelle;
			Volume = volume;
		}

	}
}
