namespace RelevésMétéo2
{
	public class DAL
	{
		private const string CHEMIN_FICHIER = "MeteoParis.csv";

		public static SortedList<DateOnly, RelevéMensuel> GetRelevésMensuels()
		{
			// Liste triée dans laquelle on va stocker les données
			SortedList<DateOnly, RelevéMensuel> relevés = new();

			// Charge les lignes du fichier dans un tableau
			string[] lignes = File.ReadAllLines(CHEMIN_FICHIER);

			// Extrait les données et les stocke dans la liste
			for (int i = 1; i < lignes.Length; i++)
			{
				// Récupère les infos de la ligne dans un tableau
				string[] infos = lignes[i].Split(';');

				// Simplifie le format des heures d'ensoleillement
				string[] soleil = infos[6].Replace("min", "").Split("h ");

				// Crée un relevé à partir des données
				RelevéMensuel rm = new()
				{
					Mois = int.Parse(infos[0]),
					Année = int.Parse(infos[1]),
					Tmin = float.Parse(infos[2]),
					Tmax = float.Parse(infos[3]),
					Tmoy = float.Parse(infos[4]),
					Vent = float.Parse(infos[5]),
					Soleil = float.Parse(soleil[0]) + float.Parse(soleil[1]) / 60,
					Pluie = float.Parse(infos[7]),
				};
				
				// Ajoute le relevé à la liste triée (clé = date, valeur = relevé)
				DateOnly dt = new DateOnly(rm.Année, rm.Mois, DateTime.DaysInMonth(rm.Année, rm.Mois));
				relevés.Add(dt, rm);
			}

			return relevés;
		}
	}
}