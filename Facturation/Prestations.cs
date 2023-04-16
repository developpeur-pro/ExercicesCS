using System.Collections.ObjectModel;

namespace Faturation
{
	public class Prestation
	{
		public int IdClient { get; set; }
		public DateTime DateDébut { get; }
		public string Intitulé { get; set; } = string.Empty;
		public decimal PrixHT { get; set; }

		public Prestation(int idClient, DateTime dateDébut, string intitulé, decimal prixHT)
		{
			IdClient = idClient;
			DateDébut = dateDébut;
			Intitulé = intitulé;
			PrixHT = prixHT;
		}
		public override string ToString()
		{
			return $"Prestation du {DateDébut:d} : {Intitulé}";
		}
	}

	public class PrestationLongTerme : Prestation
	{
		// Etapes de la prestation (date et % d'avancement)
		private List<Etape> _étapes;
		public ReadOnlyCollection<Etape> Etapes => _étapes.AsReadOnly();

		public PrestationLongTerme(int idClient, DateTime dateDébut, string intitulé, decimal prixHT) :
			base(idClient, dateDébut, intitulé, prixHT)
		{
			_étapes = new();
		}

		public void AjouterEtape(DateTime dateFin, float avancement, string libellé = "étape intermédiaire")
		{
			DateTime dateDébut = DateDébut;
			if (_étapes.Any())
				dateDébut = Etapes.Last().DateFin.AddDays(1);

			_étapes.Add(new Etape(dateDébut, dateFin, avancement, libellé));
		}

		public override string ToString()
		{
			return Etapes.Last().ToString();
		}
	}

	public class Etape
	{
		public DateTime DateDébut { get; }
		public DateTime DateFin { get; }
		public float Avancement { get; }
		public string Libellé { get; set; }

		public Etape(DateTime dateDébut, DateTime dateFin, float avancement, string libellé)
		{
			DateDébut = dateDébut;
			DateFin = dateFin;
			Avancement = avancement;
			Libellé = libellé;
		}

		public override string ToString()
		{
			return $"""
			Prestation : {Libellé}
			Période du {DateDébut:d} au {DateFin:d}
			Avancement : {Avancement:#%}
			""";
		}
	}
}
