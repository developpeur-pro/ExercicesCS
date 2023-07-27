namespace SaisieAutomatisée
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DateTime dt0 = new DateTime(2000, 12, 24);

			Console.WriteLine($"Date de départ : {dt0:D}");
			Console.WriteLine($"Premier jour du mois : {dt0.BeginningOfTheMonth():D}");
			Console.WriteLine($"Dernier jour du mois : {dt0.EndOfTheMonth():D}");
			Console.WriteLine($"Nombre d'années entières entre la date de départ et le {DateTime.Today:d} : {dt0.GetAge()}");

			int nbJours = -5;
			Console.WriteLine($"{dt0:D} + {nbJours} jours travaillés = {dt0.AddWorkedDays(nbJours):D}");

			Console.WriteLine();
			foreach (Status s in Enum.GetValues(typeof(Status)))
			{
				Console.WriteLine(s.ToDisplayableString());
			}
		}
	}
}