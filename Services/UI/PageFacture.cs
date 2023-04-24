namespace Services.UI
{
	public class PageFacture : Page
	{
		private IServiceFacture _serviceFacture;

		public PageFacture(IServiceFacture serviceFacture)
		{
			_serviceFacture = serviceFacture;
		}

		protected override void Exécuter()
		{
			// Crée un client particulier
			_serviceFacture.Client = new Particulier(Civilités.Mr, "Dupont", "Eric")
			{ Adresse = "29 rue de la liberté - 88000 Epinal" };

			// Crée une prestation classique pour ce client
			DateTime dt0 = new DateTime(2024, 5, 15);
			_serviceFacture.Prestation = new Prestation(_serviceFacture.Client.Id, dt0,
				"Déclaration de revenus 2023", 300m);

			// Facture la prestation 10 jours plus tard
			_serviceFacture.DateCréation = dt0.AddDays(10);

			// Edite la facture
			Console.WriteLine(_serviceFacture.Editer());
		}
	}
}
