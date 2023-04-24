using Services.UI;
using System.Text;

namespace Services
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.Unicode;

			// Crée la page d'accueil et l'affiche
			PageAccueil pa = new PageAccueil();
			pa.Titre = "Accueil";
			pa.Afficher();
		}
	}
}