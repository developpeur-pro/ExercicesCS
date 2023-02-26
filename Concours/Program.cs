namespace Concours
{
	internal class Program
	{
		static (string nom, double moyenne, Statuts statuts)[]? étudiants;
		const int NbAdmis = 50;

		static void Main(string[] args)
		{
			ChargerDonnées();
			AfficherRésultatsConcours();
			AfficherEtudiantsEtrangersAdmis();
			AfficherEtudiantsFrançaisBoursiers();
			RemplacerEtudiantsAdmis("Douglas Léa", "Gargamel", "Leduc Justin");
			AfficherRésultatsConcours();
			Console.ReadKey(); 
		}

		// Charge le fichier des étudiants dans un tableau de tuples
		static void ChargerDonnées()
		{
			string[] lignes = File.ReadAllLines("Etudiants.csv");

			étudiants = new (string, double, Statuts)[lignes.Length - 1];

			for (int l = 1; l < lignes.Length; l++)
			{
				string[] infos = lignes[l].Split(';');
				étudiants[l - 1].nom = infos[0] + " " + infos[1];
				étudiants[l - 1].moyenne = double.Parse(infos[4]);

				Statuts st = Statuts.Aucun;
				if (infos[2] == "O") st = Statuts.Etranger;
				if (infos[3] == "O") st |= Statuts.Boursier;
				if (l <= NbAdmis) st |= Statuts.Admis;

				étudiants[l - 1].statuts = st;
			}
		}

		// Affiche les résultats du concours (étudiants avec leurs moyennes et mentions)
		static void AfficherRésultatsConcours()
		{
			if (étudiants == null) return;

			Console.WriteLine($"Résultats du concours :\n");
			for (int i = 0; i < étudiants.Length; i++)
			{
				(Mentions mention, string libellé) mention = Notation.GetMention(étudiants[i].moyenne);
				string res = étudiants[i].statuts.HasFlag(Statuts.Admis) ? "Admis" : string.Empty;

				Console.WriteLine($"{étudiants[i].nom,-20} : {étudiants[i].moyenne,5:N1}  {mention.libellé,-12} {res}");
			}

			Console.WriteLine($"\n{NbAdmis} étudiants admis sur {étudiants.Length}");
		}

		// Affiche les noms des étudiants étranger admis à l'école
		static void AfficherEtudiantsEtrangersAdmis()
		{
			if (étudiants == null) return;

			Console.WriteLine("\nEtudiants étrangers admis :\n");
			int cpt = 0;

			for (int i = 0; i < étudiants.Length; i++)
			{
				if (étudiants[i].statuts.HasFlag(Statuts.Etranger | Statuts.Admis))
				{
					cpt++;
					Console.WriteLine($"{étudiants[i].nom,-20}");
				}
			}

			Console.WriteLine($"\nTotal : {cpt} étudiants étrangers admis");
		}

		// Affiche la liste des étudiants français boursiers
		static void AfficherEtudiantsFrançaisBoursiers()
		{
			if (étudiants == null) return;

			Console.WriteLine("\nEtudiants français boursiers :\n");
			int cpt = 0;

			for (int i = 0; i < étudiants.Length; i++)
			{
				if (!(étudiants[i].statuts.HasFlag(Statuts.Etranger)) &
					étudiants[i].statuts.HasFlag(Statuts.Boursier))
				{
					cpt++;
					Console.WriteLine($"{étudiants[i].nom,-20}");
				}
			}

			Console.WriteLine($"\nTotal : {cpt} étudiants français boursiers");
		}

		/// <summary>
		/// Remplace un ou plusieurs étudiants admis par les premiers non admis
		/// </summary>
		/// <param name="noms">noms des étudiants à remplacer</param>
		static void RemplacerEtudiantsAdmis(params string[] noms)
		{
			if (étudiants == null) return;
			Console.WriteLine();

			int cptNouveaux = 0;

			// Pour chaque étudiant à remplacer
			for (int n = 0; n < noms.Length; n++)
			{
				bool trouvé = false;

				// On recherche l'étudiant dans la liste
				for (int i = 0; i < NbAdmis; i++)
				{
					if (étudiants[i].nom == noms[n])
					{
						// On enlève le statut admis de l'étudiant
						étudiants[i].statuts ^= Statuts.Admis;

						// On ajoute le statut Admis au premier non admis
						étudiants[NbAdmis + cptNouveaux].statuts |= Statuts.Admis;

						Console.WriteLine($"Remplacement de {noms[n]} par {étudiants[NbAdmis + cptNouveaux].nom}");
						
						// On incrémente le compteurs de nouveaux admis
						cptNouveaux++;

						trouvé = true;
						break; // on sorte de la boucle
					}
				}
				if (!trouvé)
					Console.WriteLine($"Etudiant {noms[n]} non trouvé parmi les admis");
			}
			Console.WriteLine();
		}
	}
}