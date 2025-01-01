namespace Services;

public class ServiceFacture : IServiceFacture
{
	private static int cpt;
	public static readonly decimal TAUX_TVA = 0.2m;

	public IClient? Client { get; set; }
	public IPrestation? Prestation { get; set; }

	public long Numéro { get; }
	public DateTime DateCréation { get; set; }
	public int DélaiPaiement { get; set; }
	public decimal MontantHT => Prestation?.PrixHT ?? 0;
	public decimal TVA => Prestation?.PrixHT * TAUX_TVA ?? 0;
	public decimal MontantTTC => MontantHT + TVA;

	public ServiceFacture()
	{
		Numéro = ++cpt;
		DélaiPaiement = 30;
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
