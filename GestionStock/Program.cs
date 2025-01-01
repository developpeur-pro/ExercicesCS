namespace GestionStock;

internal class Program
{
	static void Main(string[] args)
	{
		// Crée un stock avec seuil d'alerte à 50;
		Stock stock = new();
		stock.SeuilAlerte = 50m;

		// Définit un gestionnaire pour l'événement
		stock.AlerteStockBas += (sender, e) =>
			Console.WriteLine($"Attention, au {e.Item1:d}, il ne reste que {e.Item2} kg en stock !");

		// Charge le fichier et crée les mouvements de stock
		Console.WriteLine("Création des mouvements de stocks");

		var mvts = File.ReadAllLines("MouvementsStock.csv");
		DateOnly jour;
		decimal qté;
		int nbMvtsCréés = 0;

		for (int l = 1; l < mvts.Length; l++)
		{
			string[] infos = mvts[l].Split(';');
			jour = DateOnly.Parse($"{infos[0]}/{DateTime.Today.Year}");
			qté = decimal.Parse(infos[1]);

			try
			{
				if (qté < 0)
					stock.Retirer(jour, -qté);
				else
					stock.Ajouter(jour, qté);

				nbMvtsCréés++;
			}
			catch (ArgumentException)
			{
				Console.WriteLine($"Erreur : il y a déjà un mouvement de stock au {jour:d}");
			}
		}

		Console.WriteLine($"{nbMvtsCréés} mouvements de stocks créés.");
		Console.WriteLine();

		// Affiche l'état du stock au 1er jour de chaque mois
		for (int m = 1; m <= 12; m++)
		{
			var date = new DateOnly(DateTime.Today.Year, m, 1);
			Console.WriteLine($"Etat du stock au {date:d} : {stock.GetEtatStock(date)} kg");
		}
	}
}