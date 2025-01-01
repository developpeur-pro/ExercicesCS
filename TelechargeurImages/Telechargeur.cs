using SixLabors.ImageSharp;
using System.Text.RegularExpressions;

namespace TelechargeurImages;

public static class Telechargeur
{
	private static readonly HttpClient client = new HttpClient();

	// Obtient la liste des url d'images jpeg de la page web passée en paramètre
	public static async Task<string[]> GetUrlsImagesAsync(string urlPage)
	{
		HttpResponseMessage reponse = await client.GetAsync(urlPage);
		reponse.EnsureSuccessStatusCode();
		string html = await reponse.Content.ReadAsStringAsync();

		// Recherche les urls d'images jpg au moyen d'une expression régulière
		var matches = Regex.Matches(html, @"=""(?<image>https://[^:]*(\.jpg|.jpeg))""");

		// Extrait les urls des résultats
		string[] urls = matches.Select(m => m.Groups["image"].Value).Distinct().ToArray();

		return urls;
	}

	// Télécharge l'image à l'url donnée et renvoie son nom
	public static async Task<string> TelechargerImageAsync(string url)
	{
		// Obtient le flux du fichier en téléchargement
		using Stream stream = await client.GetStreamAsync(url);

		// Charge l'image en mémoire depuis le flux
		Image img = await Image.LoadAsync(stream);

		// Encode l'image en webp et l'enregistre dans un fichier
		string nom = Path.GetFileNameWithoutExtension(url) + ".webp";
		// Décommenter ce code pour tester la gestion d'erreur
		if (nom == "alouette-100414-310-160.webp")
			throw new InvalidOperationException(url);

		await img.SaveAsWebpAsync(nom);

		return nom;
	}

	// Même tâche avec possibilité d'annulation
	public static async Task<string> TelechargerImageAsync(string url, CancellationToken jetonAnnul)
	{
		// Obtient le flux du fichier en téléchargement
		using Stream stream = await client.GetStreamAsync(url, jetonAnnul);

		// Charge l'image en mémoire depuis le flux
		Image img = await Image.LoadAsync(stream, jetonAnnul);

		// Encode l'image en webp et l'enregistre dans un fichier
		string nom = Path.GetFileNameWithoutExtension(url) + ".webp";
		if (nom == "alouette-100414-310-160.webp")
			throw new InvalidOperationException(url);

		await img.SaveAsWebpAsync(nom, jetonAnnul);

		return nom;
	}
}
