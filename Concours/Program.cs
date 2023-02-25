namespace Concours
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Charge le fichier des étudiants dans un tableau de tuples
			string[] lignes = File.ReadAllLines("Etudiants.csv");
			(string nom, bool étranger, bool boursier, double moyenne)[] étudiants =
				new (string, bool, bool, double)[lignes.Length];

			for (int l = 1; l < lignes.Length; l++)
			{
				string[] infos = lignes[l].Split(';');

				étudiants[l - 1] = (
					infos[0] + " " + infos[1],
					infos[2] == "O",
					infos[3] == "O",
					double.Parse(infos[4])
					);
			}

			// Affiche les étudiants reçus, avec leurs moyennes et mentions
			Console.WriteLine("Etudiants reçus au concours :\n");
			const double seuil = 14.8;
			int cpt = 0;
			for (int i = 0; i < étudiants.Length; i++)
			{
				double moy = étudiants[i].moyenne;
				if (moy >= seuil)
				{
					cpt++;
					(Mentions mention, string libellé) mention = Notation.GetMention(moy);
					Console.WriteLine($"{étudiants[i].nom,-20} : {étudiants[i].moyenne,5:N1}  {mention.libellé}");
				}
			}

			Console.WriteLine($"\nTotal : {cpt} étudiants reçus sur {étudiants.Length}.");

			Console.ReadKey();
		}
	}
}