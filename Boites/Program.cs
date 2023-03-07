using System.Xml;

namespace Boites
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Boite b1 = new Boite(20, 20, 10);
			Console.WriteLine($"Boite de volume {b1.Volume} en {b1.Matiere}");
			Console.WriteLine($"Nombre de boites : {Boite.NbBoites}");

			Boite b2 = new Boite(20, 30, 10, Matieres.Bois);
			Console.WriteLine($"Boite de volume {b2.Volume} en {b2.Matiere}");
			Console.WriteLine($"Nombre de boites : {Boite.NbBoites}");

			Console.WriteLine($"Boites identiques : {b1.Comparer(b2)}");

			Boite b3 = new Boite(20, 30, 10, Matieres.Bois);
			Console.WriteLine($"Boites identiques : {b2.Comparer(b3)}");

			b1.Etiqueter("M. Dupont", false);
			Console.WriteLine($"Boite de {b1.Destinataire} {(b1.Fragile ? "Fragile" : "Non fragile")}");

			Etiquette e1 = new Etiquette { Texte = "M. Dupont", Couleur = Couleurs.Marron, Format = Formats.L };
			Console.WriteLine($"Etiquette marquée {e1.Texte}, de couleur {e1.Couleur} et de format {e1.Format}");
		}
	}
}