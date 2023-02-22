using Microsoft.VisualBasic;

namespace CalendrierPerpetuel
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Saisie d'une année
			bool saisieOK = false;
			int année = 2025;
			while (!saisieOK)
			{
				Console.WriteLine("Saisissez une année comprise entre 1 et 9999");
				string? rep = Console.ReadLine();
				saisieOK = int.TryParse(rep, out année) && année >= 1 && année <= 9999 ;
			}
			Console.Clear();

			// Constantes de formats et séparateur
			const string format = "ddd dd MMM";
			const string format2 = "ddd dd MMM hh:mm K";
			string sep = "\n" + new string('-', 45);

			// Affichage des débuts de saisons
			(DateOnly printemps, DateOnly été, DateOnly automne, DateOnly hiver) =
				CalculateurCalendrier.CalculerDatesDébutsSaisons(année);

			Console.WriteLine($"""
				Dates des débuts de saisons de l'année {année}{sep}
				- printemps : {printemps.ToString(format)}
				- été       : {été.ToString(format)}
				- automne   : {automne.ToString(format)}
				- hiver     : {hiver.ToString(format)}
				""");

			// Affichage des jours fériés
			(DateOnly, string)[] joursFériés = CalculateurCalendrier.CalculerJoursFériésFrançais(année);
			Console.WriteLine($"\nJours fériés de l'année {année}{sep}");
			foreach ((DateOnly date, string libellé) jour in joursFériés)
			{
				Console.WriteLine($"- {jour.date.ToString(format),-13} : {jour.libellé}");
			}

			// Affichage des changements d'heures
			(DateTimeOffset heureEté, DateTimeOffset heureHiver) changements = CalculateurCalendrier.CalculerChangementsHeures(année);
			Console.WriteLine($"\nChangements d'heures de l'année {année}{sep}");
			Console.WriteLine($"- Heure d'été   : {changements.heureEté.ToString(format2)}");
			Console.WriteLine($"- Heure d'hiver : {changements.heureHiver.ToString(format2)}");
			Console.WriteLine();

			// Saisie de la date d'anniversaire
			DateOnly dateAnniv = new DateOnly();
			do
			{
				Console.WriteLine("Quelle est votre date d'anniversaire (JJ/MM) ?");
				string? anniv = Console.ReadLine();
				saisieOK = DateOnly.TryParseExact(anniv, "dd/MM", out dateAnniv);
			} while (!saisieOK);

			// Calcul du jour de la semaine de l'anniversaire pour l'année
			dateAnniv = new DateOnly(année, dateAnniv.Month, dateAnniv.Day);
			Console.WriteLine($"\nEn {année}, votre anniversaire sera un {dateAnniv.ToString("dddd")}");
		}
	}
}