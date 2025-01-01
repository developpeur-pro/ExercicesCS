namespace ModelesDocs;

internal class Program
{
	static void Main(string[] args)
	{
		var auteurs = new Personne[3];
		auteurs[1] = new Personne { Nom = "Durand", Prenom = "Léo" };
		auteurs[2] = new Personne { Nom = "Ricaud", Prenom = "Léa" };

		var nomModeles = new string[] { "Modèle A", "modèle B", "Modèle C" };

		for (int i = 0; i < auteurs.Length; i++)
		{
			Document? doc = Document.CreerDepuisModele(nomModeles[i], auteurs[i]);

			if (doc == null)
			{
				Console.WriteLine($"Aucun modèle trouvé avec le titre {nomModeles[i]}\n");
				continue;
			}

			Console.WriteLine($"""
			Document créé :
			Pied de page : {doc.PiedDePage}
			Marges : H={doc.Marges.Haut}cm B={doc.Marges.Bas}cm G={doc.Marges.Gauche}cm D={doc.Marges.Droite}cm

			""");
		}
	}
}