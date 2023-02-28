using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concours
{
	internal enum Mentions { E=0, P=10, AB=12, B=14, TB=16 }

	internal class Notation
	{
		static string[] LibellésMentions = { "Echec", "Passable", "Assez bien", "Bien", "Très bien" };
		

		public static (Mentions, string) GetMention(double note)
		{
			Mentions mention = Mentions.E;
			string libellé = LibellésMentions[0];

			int cpt = 0;
			foreach (Mentions m in Enum.GetValues(typeof(Mentions)))
			{
				if ((int)m <= note)
				{
					mention = m;
					libellé = LibellésMentions[cpt++];
				}
				else
					break;
			}

			return (mention, libellé);
		}
	}
}
