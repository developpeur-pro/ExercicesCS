using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RelevésMétéo
{
	internal class Program
	{
		static void Main(string[] args)
		{
			AfficherListe();
			Console.WriteLine();
			AfficherTableau();
			Console.ReadKey();
		}

		static void AfficherListe()
		{
			string[] lignes = File.ReadAllLines("meteoParis.csv");

			float sommeTemp = 0f;
			for (int i = 1; i < lignes.Length; i++)
			{
				// Simplifie le format des heures d'ensoleillement
				string ligne = lignes[i].Replace("h ", "h").Replace("min", "");

				// Récupère les infos de la ligne dans un tableau
				string[] infos = ligne.Split(';');

				// Construit une ligne sous la forme souhaitée
				Console.WriteLine($"{infos[0]}/{infos[1]} : [{infos[2]} ; {infos[3]}]°C\t" +
					$"{infos[6]} de soleil\t{infos[7]} mm de pluie");

				// Ajoute la température moyenne au cumul
				if (float.TryParse(infos[4], out float temp))
					sommeTemp += temp;
			}

			Console.WriteLine();
			Console.WriteLine($"T° moyenne globale : {sommeTemp / (lignes.Length - 1)}");
		}

		static void AfficherTableau()
		{
			string[] lignes = File.ReadAllLines("meteoParis.csv");

			const string EnTeteTableau = """
				   Mois | T° min | T° max | Soleil | Pluie (mm) 
				-----------------------------------------------
				""";

			Console.WriteLine(EnTeteTableau);

			for (int i = 1; i < lignes.Length; i++)
			{
				// Simplifie le format des heures d'ensoleillement
				string ligne = lignes[i].Replace("h ", "h").Replace("min", "");

				// Récupère les infos de la ligne dans un tableau
				string[] infos = ligne.Split(';');

				float.TryParse(infos[2], out float tmin);
				float.TryParse(infos[3], out float tmax);
				float.TryParse(infos[7], out float pluie);

				// Construit une ligne sous la forme souhaitée
				Console.WriteLine($"{infos[0]}/{infos[1]} | {tmin,6:F1} | {tmax,6:F1} | {infos[6],6} | {pluie,10:F1}");
			}
		}
	}
}