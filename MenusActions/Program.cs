
namespace MenusActions
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Définit un menu et ses actions associées
			List<(string lib, Action action)> menu = new()
			{
				("Quitter l'appli", () => Environment.Exit(0)),
				("Action 1", Actions.Action1),
				("Action 2", Actions.Action2),
			};

			// Excécute l'appli en boucle
			while (true)
			{
				// Récupère les libellés du menu
				var libs = menu.Select(m => m.lib);

				// Affiche le menu, récupère le choix et vide l'écran
				int choix = AfficherMenu(libs);
				Console.Clear();

				// Ecécute l'action correspondant au menu choisi
				menu[choix].action();

				// Attend l'appui sur Echap pour vider l'écran et continuer la boucle
				Console.WriteLine("\nAppuyez sur Echap pour revenir au menu...");
				while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
				Console.Clear();
			}
		}

		// Affiche le menu et demande de faire un choix
		private static int AfficherMenu(IEnumerable<string> libMenus)
		{
			int cpt = 0;
			foreach (string lib in libMenus)
			{
				Console.WriteLine($"{cpt++} : {lib}");
			}

			// Récupère et contrôle le choix
			Console.WriteLine("\nVotre choix ?");
			int choix = 0;
			bool choixOK = false;
			while (!choixOK)
			{
				string? rep = Console.ReadLine();
				choixOK = int.TryParse(rep, out choix) && choix >= 0 & choix < cpt;
			}

			return choix;
		}
	}
}