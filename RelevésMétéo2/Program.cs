namespace RelevésMétéo2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TesterDico();
			//CalculerStats1();
			//CalculerStats2();
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
				$"et {relevés.GetKeyAtIndex(relevés.Count - 1):MMM yyyy} :\n");

			// Affiche le contenu du dico de stats
			foreach (var kvp in stats)
			{
				Console.WriteLine($"{kvp.Key} : {kvp.Value.Item1,4:N1} °C, {kvp.Value.Item2,5:N1} mm");
			}
		}

		static void CalculerStats1()
		{
			IList<RelevéMensuel> relevés = DAL.GetRelevésMensuels().Values;

			// 1/ Mois où la T° min a été <= 0°C  et le vent >= 80 km/h
			var req1 = from r in relevés
						  where r.Tmin <= 0 && r.Vent >= 80
						  select r;

			var req10 = relevés.Where(r => r.Tmin <= 0 && r.Vent >= 80);

			Console.WriteLine("Mois où la T° min a été <= 0 °C et le vent >= 80 km/h :");
			foreach (RelevéMensuel r in req1)
			{
				Console.WriteLine($"{r.Mois:00}/{r.Année} : {r.Tmin} °C, {r.Vent} km/h");
			}

			// 2/ Relevés de l'année 2022 classés par T° moyenne décroissante 
			var req2 = from r in relevés
						  where r.Année == 2022
						  orderby r.Tmoy descending
						  select r;

			var req20 = relevés.Where(r => r.Année == 2022).OrderByDescending(r => r.Tmoy);

			Console.WriteLine("\nRelevés de l'année 2022 classés par T° moyenne décroissante :");
			foreach (RelevéMensuel r in req2)
			{
				Console.WriteLine($"{r.Mois:00}/{r.Année} : {r.Tmoy} °C");
			}

			// 3/ Température maximale
			RelevéMensuel rm = (from r in relevés
									  orderby r.Tmax
									  select r).Last();

			RelevéMensuel rm2 = relevés.OrderBy(r => r.Tmax).Last();

			Console.WriteLine($"\nT° maxi atteinte en {rm.Mois:00}/{rm.Année} : {rm.Tmax}°C");
		}

		static void CalculerStats2()
		{
			var relevés = DAL.GetRelevésMensuels().Values;

			// 1/ Durée moyenne d'ensoleillement des mois de Juillet
			float soleil = (from r in relevés
								 where r.Mois == 7
								 select r.Soleil).Average();

			float soleil2 = relevés.Where(r => r.Mois == 7).Average(r => r.Soleil);

			Console.WriteLine($"\nDurée moyenne d'ensoleillement des mois de Juillet : {soleil:F1} h");

			// 2/ Cumul de pluie par année
			var req2 = from r in relevés
						 group r by r.Année into groupes
						 select (groupes.Key, groupes.Sum(r => r.Pluie));

			var req20 = relevés.GroupBy(r => r.Année)
				.Select(g => (g.Key, g.Sum(r => r.Pluie)));

			Console.WriteLine($"\nCumul de pluie par année :");
			foreach ((int année, float pluie) in req2)
			{
				Console.WriteLine($"{année} : {pluie:N1} mm");
			}

			// 3/ Température moyenne et cumul de pluie par trimestre
			var req3 = from r in relevés
						group r by $"T{(r.Mois - 1) / 3 + 1}-{r.Année}" into grp
						select (grp.Key, grp.Average(r => r.Tmoy), grp.Sum(r => r.Pluie));

			var req30 = relevés.GroupBy(r => $"T{(r.Mois - 1) / 3 + 1}-{r.Année}")
				.Select(g => (g.Key, g.Average(r => r.Tmoy), g.Sum(r => r.Pluie)));

			Console.WriteLine("\nTempérature moyenne et cumul de pluie par trimestre :");
			foreach ((string trim, float temp, float pluie) in req3)
			{
				Console.WriteLine($"{trim} : {temp,4:N1} °C, {pluie,5:N1} mm");
			}

			// 4/ Classement des années par température moyenne décroissante
			var req4 = from r in relevés
						  group r by r.Année into groupes
						  orderby groupes.Average(r => r.Tmoy) descending
						  where groupes.Count() == 12
						  select (groupes.Key, groupes.Average(r => r.Tmoy));

			var req40 = relevés.GroupBy(r => r.Année)
				.OrderByDescending(g => g.Average(r => r.Tmoy))
				.Where(g => g.Count() == 12)
				.Select(g => (g.Key, g.Average(r => r.Tmoy)));

			Console.WriteLine($"\nClassement des années par température moyenne :");
			foreach ((int année, float temp) in req4)
			{
				Console.WriteLine($"{année} : {temp:N1} °C");
			}
		}
	}
}