using System.Diagnostics;

namespace TelechargeurImages;

internal class Program
{
	static async Task Main(string[] args)
	{
		// Définit le dossier "Mes images\ImagesWeb" comme dossier de travail
		string mesImages = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
		Directory.SetCurrentDirectory(Path.Combine(mesImages, "ImagesWeb"));

		// Récupère toutes les urls des images contenues dans la page web souaitée
		string urlPage = "https://jardinage.lemonde.fr/dossiers-cat2-36-oiseaux.html";
		string[] urls = await Telechargeur.GetUrlsImagesAsync(urlPage);

		Stopwatch sw = new();
		sw.Start();
		//await TelechargerImagesUneParUneAsync(urls);
		//await TelechargerImagesEnParalleleAsync(urls);
		//await TelechargerImagesEnParallele2Async(urls);
		await TelechargerImagesAvecAnnulationAsync(urls);
		sw.Stop();
		Console.WriteLine($"\nTemps d'exécution : {sw.ElapsedMilliseconds}");
		sw.Reset();
	}

	private static async Task TelechargerImagesUneParUneAsync(string[] urls)
	{
		// Télécharge et enregistre les images une par une
		foreach (string url in urls)
		{
			try
			{
				string nom = await Telechargeur.TelechargerImageAsync(url);
				Console.WriteLine("Image téléchargée : " + nom);
			}
			catch (Exception e)
			{
				Console.WriteLine("Image non téléchargée : " + e.Message);
			}
		}
	}

	private static async Task TelechargerImagesEnParalleleAsync(string[] urls)
	{
		// Lance les tâches pour télécharger et enregistrer les images
		List<Task<string>> taches = new();
		foreach (string url in urls)
		{
			taches.Add(Telechargeur.TelechargerImageAsync(url));
		}

		// ...puis attend leurs résultats et les affiche dans l'ordre de la liste
		foreach (Task<string> tache in taches)
		{
			try
			{
				string nom = await tache;
				Console.WriteLine("Image téléchargée : " + nom);
			}
			catch (Exception e)
			{
				Console.WriteLine("Image non téléchargée : " + e.Message);
			}
		}
	}

	private static async Task TelechargerImagesEnParallele2Async(string[] urls)
	{
		// Lance les tâches pour télécharger et enregistrer les images
		List<Task<string>> taches = new();
		foreach (string url in urls)
		{
			taches.Add(Telechargeur.TelechargerImageAsync(url));
		}

		// ...puis affiche leurs résultats dans l'ordre où elles se terminent
		while (taches.Any())
		{
			Task<string> tache = await Task.WhenAny(taches);
			try
			{
				Console.WriteLine("Image téléchargée : " + tache.Result);
			}
			catch (AggregateException ae)
			{
				foreach (Exception e in ae.InnerExceptions)
				{
					Console.WriteLine($"Image non téléchargée : {e.Message}");
				}
			}
			taches.Remove(tache);
		}
	}

	private static async Task TelechargerImagesAvecAnnulationAsync(string[] urls)
	{
		Console.WriteLine("Appuyez sur une touche pour lancer le téléchargement, et sur Echap pour l'annuler");
		Console.ReadKey(true);

		CancellationTokenSource cts = new();
		_ = Task.Run(() =>
		{
			while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
			cts.Cancel();
		});

		// Lance les tâches pour télécharger et enregistrer les images
		List<Task<string>> taches = new();
		foreach (string url in urls)
		{
			taches.Add(Telechargeur.TelechargerImageAsync(url, cts.Token));
		}

		// ...puis affiche leurs résultats dans l'ordre où elles se terminent
		while (taches.Any())
		{
			Task<string> tache = await Task.WhenAny(taches);
			try
			{
				Console.WriteLine("Image téléchargée : " + tache.Result);
			}
			catch (AggregateException ae)
			{
				foreach (Exception e in ae.InnerExceptions)
				{
					if (e is OperationCanceledException)
						Console.WriteLine("Téléchargement annulé");
					else
						Console.WriteLine($"Image non téléchargée : {e.Message}");
				}
			}

			taches.Remove(tache);
		}
	}
}
