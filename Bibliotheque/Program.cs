namespace Bibliotheque
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string chemin = Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.Personal),
				"Bibliotheque", "Livres.txt");

			if (!File.Exists(chemin))
			{
				Console.WriteLine($"le fichier {chemin} n'existe pas");
				return;
			}

			List<Livre> livres = DAL.GetLivres(chemin);
			
			HTMLWriter.GénérerPage(livres, Path.ChangeExtension(chemin, ".htm"));

			DAL.ExporterLivresJSON(livres, Path.ChangeExtension(chemin, ".json"));
			DAL.ExporterLivresCSV(livres, Path.ChangeExtension(chemin, ".csv"));

			Console.WriteLine($"Page de {livres.Count} livres générée");
		}
	}
}