using System.Text;

namespace Faturation;

internal class Program
{
	static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.Unicode;
		//TesterClientsPresta();
		TesterFacturation();
	}

	static void TesterClientsPresta()
	{
		//-------------------------------------------------------------
		// Prestation classique à un particulier

		// Crée un client particulier
		Particulier p = new(Civilités.Mr, "Dupont", "Eric")
		{ Adresse = "29 rue de la liberté - 88000 Epinal" };

		// Crée une prestation classique pour ce client
		DateTime dt0 = new DateTime(2024, 5, 15);
		Prestation presta = new(p.Id, dt0, "Déclaration de revenus 2023", 300m);

		// Affiche l'ensemble
		Console.WriteLine($"""
			Client N°{p.Id} : {p.Civilité} {p.Prénom} {p.Nom}
			Prestation : {presta.Intitulé} 
			Prix : {presta.PrixHT:C2}
			Date : {presta.DateDébut:d}
			""");

		//-------------------------------------------------------------
		// Prestation long terme à une entreprise

		// Crée un client de type entreprise
		Entreprise e = new("Microsoft France", 32773318400516)
		{ Adresse = "39 quai du Président Roosevelt, 92130 Issy-les-Moulineaux" };

		// Crée une prestation long terme pour ce client
		DateTime dt = new DateTime(2024, 1, 1);
		PrestationLongTerme plt = new(e.Id, dt, "Compta trimestrielle", 2000m);

		// Crée les étapes mensuelles
		for (int i = 1; i <= 4; i++)
		{
			DateTime dateFinEtape = dt.AddMonths(3 * i).AddDays(-1);
			plt.AjouterEtape(dateFinEtape, i / 4f, $"Compta du trimestre {i}");
		}

		// Affiche l'ensemble
		Console.WriteLine($"""

			Client N°{e.Id} : {e.RaisonSociale}, SIRET : {e.SIRET}
			Prestation : {plt.Intitulé} ({plt.Etapes.Count} étapes)
			Prix : {plt.PrixHT:C2}
			Date début : {plt.DateDébut:d}
			Etapes :
			""");

		foreach (Etape etape in plt.Etapes)
		{
			Console.WriteLine($"- {etape.Libellé} du {etape.DateDébut:d} au {etape.DateFin:d} ({etape.Avancement:##%})");
		}
	}

	static void TesterFacturation()
	{
		//-------------------------------------------------------------
		// Facturation d'une prestation classique à un particulier
		
		// Crée un client particulier
		Particulier p = new(Civilités.Mr, "Dupont", "Eric") 
		{ Adresse = "29 rue de la liberté - 88000 Epinal" };

		// Crée une prestation classique pour ce client
		DateTime dt0 = new DateTime(2024, 5, 15);
		Prestation presta = new(p.Id, dt0, "Déclaration de revenus 2023", 300m);

		// Facture la prestation 10 jours plus tard
		Facture facture = new Facture(p, presta, dt0.AddDays(10));

		// Créer et affiche la facture
		Console.WriteLine(facture.Editer());

		//-------------------------------------------------------------
		// Facturation des étapes d'une prestation long terme à une entreprise

		// Crée un client de type entreprise
		Entreprise e = new("Microsoft France", 32773318400516)
		{ Adresse = "39 quai du Président Roosevelt, 92130 Issy-les-Moulineaux"};

		// Crée une prestation long terme pour ce client
		DateTime dt = new DateTime(2024, 1, 1);
		PrestationLongTerme plt = new(e.Id, dt, "Compta trimestrielle", 2000m);

		// Crée les étapes mensuelles et les factures de situation correspondantes
		for (int i = 1; i <= 4; i++)
		{
			DateTime dateFinEtape = dt.AddMonths(3*i).AddDays(-1);
			plt.AjouterEtape(dateFinEtape, i/4f, $"Compta du trimestre {i}");
			FactureSituation fs = new FactureSituation(e, plt, dateFinEtape.AddDays(10));

			Console.WriteLine(fs.Editer());
		}
	}
}