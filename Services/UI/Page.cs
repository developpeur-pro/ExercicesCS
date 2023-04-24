namespace Services.UI
{
	public interface IPage
	{
		string Titre { get; set; }
		IPage? Parente { get; set; }
		void Afficher();
	}

	public abstract class Page : IPage
	{
		public string Titre { get; set; } = string.Empty;
		public IPage? Parente { get; set; }

		/// <summary>
		/// Affiche la page et attend un appui sur Echap pour revenir à la page parente
		/// </summary>
		public virtual void Afficher()
		{
			// Vide la console et affiche le titre de la page
			Console.Clear();
			Console.WriteLine(Titre);
			Console.WriteLine(new string('-', 30));

			// Exécute la logique de la page
			Exécuter();

			// Attend l'appui sur Echap pour afficher la page parente si elle existe
			Console.WriteLine("\nAppuyez sur Echap pour revenir à la page précédente...");
			while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;

			Parente?.Afficher();
		}

		protected abstract void Exécuter();
	}
}
