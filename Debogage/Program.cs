namespace Debogage;

internal class Program
{
	static void Main(string[] args)
	{
		List<Etudiant> étudiants = DAL.ChargerDonnées();
		AfficherRésultatsConcours(étudiants);
		Console.ReadKey();
		Console.Clear();

		RechercherEtudiantParNom(étudiants, "Leduc");

		Console.ReadKey();
	}

	/// <summary>
	/// Affiche le texte passé en paramètre avec la couleur spécifiée
	/// </summary>
	/// <param name="texte">texte à afficher</param>
	/// <param name="couleur">couleur de police à utiliser</param>
	static void AfficherTexte(string texte, ConsoleColor couleur = ConsoleColor.Blue)
	{
		ConsoleColor couleurOrigine = Console.ForegroundColor;
		Console.ForegroundColor = couleur;
		Console.WriteLine(texte);
		Console.ForegroundColor = couleurOrigine;
	}

	// Affiche les résultats du concours (étudiants avec leurs moyennes et mentions)
	static void AfficherRésultatsConcours(List<Etudiant> étudiants)
	{
		AfficherTexte($"Résultats du concours :\n");
		foreach (Etudiant e in étudiants)
		{
			string res = e.Statut.HasFlag(Statuts.Admis) ? "Admis" : string.Empty;

			Console.WriteLine($"{e.Nom,-15} {e.Prénom,-15}: {e.Moyenne,5:N1}  {res}");
		}

		AfficherTexte($"\n{DAL.NbAdmis} étudiants admis sur {étudiants.Count}", ConsoleColor.DarkGreen);
	}

	/// <summary>
	/// Recherche un étudiant par son nom et affiche ses infos
	/// </summary>
	/// <param name="étudiants">liste des étudiants</param>
	/// <param name="recherche">Nom recherché</param>
	static void RechercherEtudiantParNom(List<Etudiant> étudiants, string recherche)
	{
		AfficherTexte($"Recherche de l'étudiant(e) {recherche}:\n");

		Etudiant? res = étudiants.Find(e => e.Nom.StartsWith(recherche));

		if (res != null)
			Console.WriteLine($"{res.Nom} {res.Prénom}, moyenne = {res.Moyenne}");
		else
			Console.WriteLine("Aucun étudiant ne correspond à cette recherche");
	}
}