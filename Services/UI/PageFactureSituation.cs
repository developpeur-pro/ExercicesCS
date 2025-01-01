namespace Services.UI;

public class PageFactureSituation : Page
{
	private IServiceFacture _serviceFacture;

	public PageFactureSituation(IServiceFacture serviceFacture)
	{
		_serviceFacture = serviceFacture;
	}

	protected override void Exécuter()
	{
		// Crée un client de type entreprise
		Entreprise e = new("Microsoft France", 32773318400516)
		{ Adresse = "39 quai du Président Roosevelt, 92130 Issy-les-Moulineaux" };

		// Crée une prestation long terme pour ce client
		DateTime dt = new DateTime(2024, 1, 1);
		PrestationLongTerme plt = new(e.Id, dt, "Compta trimestrielle", 2000m);

		// Crée les étapes mensuelles et les factures de situation correspondantes
		for (int i = 1; i <= 4; i++)
		{
			DateTime dateFinEtape = dt.AddMonths(3 * i).AddDays(-1);
			plt.AjouterEtape(dateFinEtape, i / 4f, $"Compta du trimestre {i}");

			_serviceFacture.Client = e;
			_serviceFacture.Prestation = plt;
			_serviceFacture.DateCréation = dateFinEtape.AddDays(10);

			Console.WriteLine(_serviceFacture.Editer());
		}
	}
}
