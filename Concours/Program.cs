namespace Concours
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DAL.ChargerDonnées();
			AfficherRésultatsConcours();
			Console.ReadKey();
			Console.Clear();

			AfficherEtudiantsEtrangersAdmis();

			Console.ReadKey();
			Console.Clear();

			AfficherEtudiantsFrançaisBoursiers();

			Console.ReadKey();
			Console.Clear();

			string[] remplacés = { "Douglas Léa", "Cartier Claude", "Leduc Justin" };
			string[] remplaçants = DAL.RemplacerEtudiantsAdmis(remplacés);
			for (int r = 0; r < remplacés.Length; r++)
			{
				Console.WriteLine($"Remplacement de {remplacés[r]} par {remplaçants[r]}");
			}
			Console.WriteLine();
			AfficherRésultatsConcours();
			Console.ReadKey();
		}

		/// <summary>
		/// Affiche le texte passé en paramètre avec la couleur spécifiée
		/// </summary>
		/// <param name="texte">texte à afficher</param>
		/// <param name="couleur">couleur de police à utiliser</param>
		static void AfficherTexte(string texte, ConsoleColor couleur = ConsoleColor.Blue)
		{
			ConsoleColor couleurOrigine = Console.ForegroundColor;
			Console.ForegroundColor = couleur;
			Console.WriteLine(texte);
			Console.ForegroundColor = couleurOrigine;
		}

		// Affiche les résultats du concours (étudiants avec leurs moyennes et mentions)
		static void AfficherRésultatsConcours()
		{
			if (DAL.Etudiants == null) return;

			AfficherTexte($"Résultats du concours :\n");
			for (int i = 0; i < DAL.Etudiants.Length; i++)
			{
				(Mentions mention, string libellé) mention = Notation.GetMention(DAL.Etudiants[i].moyenne);
				string res = DAL.Etudiants[i].statut.HasFlag(Statuts.Admis) ? "Admis" : string.Empty;

				Console.WriteLine($"{DAL.Etudiants[i].nom,-20} : {DAL.Etudiants[i].moyenne,5:N1}  {mention.libellé,-12} {res}");
			}

			AfficherTexte($"\n{DAL.NbAdmis} étudiants admis sur {DAL.Etudiants.Length}", ConsoleColor.DarkGreen);
		}

		// Affiche les noms des étudiants étranger admis à l'école
		static void AfficherEtudiantsEtrangersAdmis()
		{
			if (DAL.Etudiants == null) return;

			AfficherTexte("Etudiants étrangers admis :\n");
			int cpt = 0;

			for (int i = 0; i < DAL.Etudiants.Length; i++)
			{
				if (DAL.Etudiants[i].statut.HasFlag(Statuts.Etranger | Statuts.Admis))
				{
					cpt++;
					Console.WriteLine($"{DAL.Etudiants[i].nom,-20}");
				}
			}

			AfficherTexte($"\nTotal : {cpt} étudiants étrangers admis", ConsoleColor.DarkGreen);
		}

		// Affiche la liste des étudiants français boursiers
		static void AfficherEtudiantsFrançaisBoursiers()
		{
			if (DAL.Etudiants == null) return;

			AfficherTexte("Etudiants français boursiers :\n");
			int cpt = 0;

			for (int i = 0; i < DAL.Etudiants.Length; i++)
			{
				if (!DAL.Etudiants[i].statut.HasFlag(Statuts.Etranger) &
					DAL.Etudiants[i].statut.HasFlag(Statuts.Boursier))
				{
					cpt++;
					Console.WriteLine($"{DAL.Etudiants[i].nom,-20}");
				}
			}

			AfficherTexte($"\nTotal : {cpt} étudiants français boursiers", ConsoleColor.DarkGreen);
		}
	}
}