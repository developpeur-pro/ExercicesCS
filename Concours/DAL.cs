using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concours
{
	[Flags] internal enum Statuts { Aucun = 0, Etranger = 1, Boursier = 2, Admis = 4 }

	internal class DAL
	{
		public static (string nom, double moyenne, Statuts statut)[]? Etudiants;
		public const int NbAdmis = 50;

		// Charge le fichier des étudiants dans un tableau de tuples
		public static void ChargerDonnées()
		{
			string[] lignes = File.ReadAllLines("Etudiants.csv");

			Etudiants = new (string, double, Statuts)[lignes.Length - 1];

			for (int l = 1; l < lignes.Length; l++)
			{
				string[] infos = lignes[l].Split(';');
				Etudiants[l - 1].nom = infos[0] + " " + infos[1];
				Etudiants[l - 1].moyenne = double.Parse(infos[4]);

				Statuts st = Statuts.Aucun;
				if (infos[2] == "O") st |= Statuts.Etranger;
				if (infos[3] == "O") st |= Statuts.Boursier;
				if (l <= NbAdmis) st |= Statuts.Admis;

				Etudiants[l - 1].statut = st;
			}
		}

		/// <summary>
		/// Remplace un ou plusieurs étudiants admis par les premiers non admis
		/// </summary>
		/// <param name="noms">noms des étudiants à remplacer</param>
		/// <returns>Tableau des noms des remplaçants</returns>
		public static string[] RemplacerEtudiantsAdmis(params string[] noms)
		{
			// Initialise le tableau et le compteur de remplaçants
			string[] remplaçants = new string[noms.Length];
			int cptRemp = 0;

			if (Etudiants == null) return remplaçants;

			// Pour chaque étudiant à remplacer
			for (int n = 0; n < noms.Length; n++)
			{
				// On recherche l'étudiant dans la liste
				for (int i = 0; i < NbAdmis; i++)
				{
					if (Etudiants[i].nom == noms[n])
					{
						// On enlève le statut admis de l'étudiant
						Etudiants[i].statut ^= Statuts.Admis;

						// On ajoute le statut Admis au premier non admis
						// et on enregistre son nom dans le tableau des remplaçants
						Etudiants[NbAdmis + cptRemp].statut |= Statuts.Admis;
						remplaçants[n] = Etudiants[NbAdmis + cptRemp].nom;

						// On incrémente le compteurs de remplaçants et on sort de la boucle
						cptRemp++;
						break;
					}
				}
			}
			return remplaçants;
		}
	}
}
