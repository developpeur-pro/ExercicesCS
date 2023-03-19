namespace ModelesDocs
{
	internal class Document
	{
		#region données et constructeur statiques
		private static readonly List<Document> Modeles;

		static Document()
		{
			Modeles = new List<Document>();
			Modeles.Add(new Document
			{
				Titre = "Modèle A",
				DateCreation = new DateTime(2023, 1, 1),
				Marges = (2.5, 2.5, 1.5, 1.5),
			});

			Modeles.Add(new Document
			{
				Titre = "Modèle B",
				DateCreation = new DateTime(2023, 6, 30),
				Marges = (2, 2, 1, 1),
			});
		}
		#endregion

		#region Propriétés
		public string Titre { get; set; } = string.Empty;
		public Personne? Auteur { get; set; }
		public DateTime DateCreation { get; set; } = DateTime.Now;
		public (double Haut, double Bas, double Gauche, double Droite) Marges { get; set; }
		public string PiedDePage =>
			$"{Auteur?.Prenom} {Auteur?.Nom ?? "Société XYZ"} - {Titre} - créé le : {DateCreation:d}";
		#endregion

		#region Méthodes publiques
		public static Document? CreerDepuisModele(string titreModele, Personne? auteur = null)
		{
			Document? doc = null;

			// Recherche le modèle ayant le titre souhaité
			Document? modele = Modeles.Find(m => m.Titre.ToLower() == titreModele.ToLower());

			// Si on a trouvé un modèle, on crée un doc avec les mêmes titre et marge
			if (modele != null)
			{
				doc = new Document
				{
					Titre = modele.Titre,
					Auteur = auteur,
					Marges = modele.Marges,
				};
			}
			return doc;
		}
		#endregion
	}

	internal class Personne
	{
		public required string Nom { get; init; }
		public required string Prenom { get; init; }
	}
}
