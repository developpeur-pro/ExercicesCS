namespace Services.UI
{
	public class PageAccueil : Page
	{
		private readonly string[] _titresMenus = {
			"Quitter l'application",
			"Factures simples",
			"Factures de situation"
		};

		protected override void Exécuter()
		{
			// Affiche le menu
			for (int i = 0; i < _titresMenus.Length; i++)
			{
				Console.WriteLine($"{i} : {_titresMenus[i]}");
			}

			Console.WriteLine("\nVotre choix ?");

			// Récupère et contrôle le choix
			int choix = 0;
			bool choixOK = false;
			while (!choixOK)
			{
				string? rep = Console.ReadLine();
				choixOK = int.TryParse(rep, out choix) && choix >= 0 & choix <= 2;
			}

			if (choix == 0)
				Environment.Exit(0); // quitte l'appli

			// Crée la page correspondant au menu choisi
			IPage page;
			IServiceFacture service = new ServiceFacture();
			if (choix == 1)
				page = new PageFacture(service);
			else
				page = new PageFactureSituation(service);

			page.Titre = _titresMenus[choix];
			page.Parente = this;
			page.Afficher();
		}
	}
}
