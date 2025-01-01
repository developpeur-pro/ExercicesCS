namespace Faturation;

public class Facture
{
	private static int cpt;
	public static readonly decimal TAUX_TVA = 0.2m;

	public Client Client { get; }
	public Prestation Prestation { get; }

	public long Numéro { get; }
	public DateTime DateCréation { get; }
	public int DélaiPaiement { get; set; }
	public decimal MontantHT => Prestation.PrixHT;
	public decimal TVA => Prestation.PrixHT*TAUX_TVA;
	public decimal MontantTTC => MontantHT + TVA;

	public Facture(Client client, Prestation presta, DateTime dateCréation)
	{
		Numéro = ++cpt;
		DateCréation = dateCréation;
		DélaiPaiement = 30;
		Client = client;
		Prestation = presta;
	}

	public string Editer()
	{
		string entête = $"""
			------------------------------------------
			Facture N°{Numéro} du {DateCréation:d}

			Emetteur :
			Société ABC
			3 avenue des champs Elysées - 75008 Paris
			Siren : 687 456 321

			Client :
			{Client}
			""";

		string prix = $"""
		  Prix HT : {MontantHT,11:C2}
		      TVA : {TVA,11:C2}
		Total TTC : {MontantTTC,11:C2}
		""";

		return $"{entête}\n\n{Prestation}\n\n{prix}";
	}
}

public class FactureSituation : Facture
{
	public FactureSituation(Client client, PrestationLongTerme presta, DateTime dateCréation) :
		base(client, presta, dateCréation)
	{

	}
}
