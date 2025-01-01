using System.Text;
using System.Text.Json;

namespace Bibliotheque;

public static class DAL
{
	/// <summary>
	/// Charge une liste de livres depuis un fichier texte
	/// </summary>
	/// <param name="chemin">chemin complet du fichier texte</param>
	/// <returns>liste de livres</returns>
	public static List<Livre> GetLivres(string chemin)
	{
		List<Livre> livres = new();
		Livre livre = new();
		string? ligne;

		using StreamReader reader = new(chemin);

		while ((ligne = reader.ReadLine()) != null)
		{
			if (ligne.StartsWith("ISBN"))
				livre.ISBN = ligne.Substring(7);
			else if (ligne.StartsWith("Titre"))
				livre.Titre = ligne.Substring(8);
			else if (ligne.StartsWith("Auteur"))
				livre.Auteur = ligne.Substring(9);
			else if (ligne.StartsWith("Image"))
				livre.NomImage = ligne.Substring(8);
			else if (ligne.StartsWith("Publication"))
				livre.Publication = DateOnly.Parse(ligne.Substring(14));
			else if (ligne.StartsWith("Description"))
			{
				livre.Description = ligne.Substring(14);
				livres.Add(livre with { }); // ajoute une copie du livre à la liste
			}
		}

		return livres;
	}

	/// <summary>
	/// Exporte le contenu d'une liste de livres dans un fichier JSON
	/// </summary>
	/// <param name="livres">Liste de livres à exporter</param>
	/// <param name="chemin">chemin complet du fichier JSON à générer</param>
	public static void ExporterLivresJSON(List<Livre> livres, string chemin)
	{
		using FileStream fs = File.Create(chemin);
		var options = new JsonSerializerOptions { WriteIndented = true };
		JsonSerializer.Serialize(fs, livres, options);
	}

	/// <summary>
	/// Exporte le contenu d'une liste de livres dans un fichier CSV
	/// </summary>
	/// <param name="livres">Liste de livres à exporter</param>
	/// <param name="chemin">chemin complet du fichier CSV à générer</param>
	public static void ExporterLivresCSV(List<Livre> livres, string chemin)
	{
		using StreamWriter writer = new(chemin, false, Encoding.UTF8);
		writer.WriteLine("ISBN;Titre;Auteur;NomImage;Publication;Description");
		foreach (Livre l in livres)
		{
			writer.WriteLine($"{l.ISBN};{l.Titre};{l.Auteur};{l.NomImage};" +
				$"{l.Publication};{l.Description}");
		}
	}
}

public record class Livre
{
	public string ISBN { get; set; } = string.Empty;
	public string Titre { get; set; } = string.Empty;
	public string Auteur { get; set; } = string.Empty;
	public string NomImage { get; set; } = string.Empty;
	public DateOnly Publication { get; set; }
	public string Description { get; set; } = string.Empty;
}
