using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debogage
{
	[Flags] internal enum Statuts { Aucun = 0, Etranger = 1, Boursier = 2, Admis = 4 }

	internal static class DAL
	{
		public static readonly int NbAdmis = 50;
		private static readonly string NOM_FICHIER = "Etudiants.csv";

		/// <summary>
		/// Charge le fichier des étudiants dans une liste et renvoie cette liste
		/// </summary>
		/// <returns>Liste d'étudiants</returns>
		public static List<Etudiant> ChargerDonnées()
		{
			List<Etudiant> étudiants = new();

			string[] lignes = File.ReadAllLines(NOM_FICHIER);

			for (int l = 1; l < lignes.Length; l++)
			{
				string[] infos = lignes[l].Split(';');

				Statuts st = Statuts.Aucun;
				if (infos[2] == "O") st |= Statuts.Etranger;
				if (infos[3] == "O") st |= Statuts.Boursier;
				if (l <= NbAdmis) st |= Statuts.Admis;
				
				Etudiant e = new() {
					Nom = infos[0],
					Prénom = infos[1],
					Moyenne = double.Parse(infos[4]),
					Statut = st
				};

				étudiants.Add(e);
			}

			return étudiants;
		}
	}

	internal class Etudiant
	{
		public required string Nom { get; init; }
		public required string Prénom { get; init; }
		public double Moyenne { get; set; }
		public Statuts Statut { get; set; }
	}
}
