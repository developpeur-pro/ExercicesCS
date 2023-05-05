namespace RelevésMétéo2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TesterDico();
		}

		static void TesterDico()
		{
			var relevés = DAL.GetRelevésMensuels();

			// Affiche les relevés sous forme de tableau
			Console.WriteLine(RelevéMensuel.EnTeteTableau);
			foreach (var kvp in relevés)
			{
				Console.WriteLine(kvp.Value);
			}

			// Calcule la température moyenne et le cumul de pluie par trimestre
			Dictionary<string, (float, float)> stats = new(); // dico pour les résultats
			float cumulTemp = 0f, cumulPluie = 0f; // cumuls pour un trimestre
			int nbMois = 0;

			foreach (var kvp in relevés)
			{
				cumulTemp += kvp.Value.Tmoy;
				cumulPluie += kvp.Value.Pluie;
				nbMois++;

				// Si le mois est multiple de 3, c'est que le trimestre est terminé
				if (kvp.Value.Mois % 3 == 0)
				{
					// Calcule le numéro du trimestre
					int numTrimestre = (kvp.Value.Mois - 1) / 3 + 1;
					string trimestre = $"T{numTrimestre}-{kvp.Value.Année}";

					// Stocke les stats du trimestre dans le dico
					stats.Add(trimestre, (cumulTemp / nbMois, cumulPluie));

					// Remet les cumuls à 0
					cumulTemp = 0f;
					cumulPluie = 0f;
					nbMois = 0;
				}
			}

			// Récupère les mois de début et de fin depuis la liste triée des relevés
			Console.WriteLine($"\nStatistiques trimestrielles entre {relevés.GetKeyAtIndex(0):MMM yyyy} " +
				$"et {relevés.GetKeyAtIndex(relevés.Count-1):MMM yyyy} :\n");

			// Affiche le contenu du dico de stats
			foreach (var kvp in stats)
			{
				Console.WriteLine($"{kvp.Key} : {kvp.Value.Item1,4:N1} °C, {kvp.Value.Item2,5:N1} mm");
			}
		}
	}
}