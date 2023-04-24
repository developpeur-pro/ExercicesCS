using System.Collections.ObjectModel;

namespace Services
{
	public interface IPrestation
	{
		int IdClient { get; set; }
		DateTime DateDébut { get; }
		string Intitulé { get; set; }
		decimal PrixHT { get; set; }
	}

	public interface IPrestationLongTerme : IPrestation
	{
		ReadOnlyCollection<Etape> Etapes { get; }
		void AjouterEtape(DateTime dateFin, float avancement, string libellé);
	}

	public interface IClient
	{
		public int Id { get; }
		public string NomComplet { get; }
		public string Adresse { get; set; }
	}

	public interface IServiceFacture
	{
		long Numéro { get; }
		DateTime DateCréation { get; set; }
		IClient? Client { get; set; }
		IPrestation? Prestation { get; set; }
		string Editer();
	}
}
