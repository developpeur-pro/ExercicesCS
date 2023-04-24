namespace Services
{
	public abstract class Client : IClient
	{
		private static int _compteur;

		public int Id { get; }
		public virtual string NomComplet { get; protected set; } = string.Empty;
		public string Adresse { get; set; } = string.Empty;

		public Client()
		{
			Id = ++_compteur;
		}

		public override string ToString()
		{
			return $"""
				Référence : {Id}
				{NomComplet}
				Adresse : {Adresse}
				""";
		}
	}

	public enum Civilités { Mme, Mr };

	public class Particulier : Client
	{
		public Civilités Civilité { get; set; }
		public string Nom { get; set; }
		public string Prénom { get; set; }
		public override string NomComplet => $"{Civilité} {Nom} {Prénom}";

		public Particulier(Civilités civilité, string nom, string prénom)
		{
			Civilité = civilité;
			Nom = nom;
			Prénom = prénom;
		}
	}

	public class Entreprise : Client
	{
		public string RaisonSociale { get; set; }
		public long SIRET { get; set; }
		public override string NomComplet => $"Société {RaisonSociale}";

		public Entreprise(string raisonSociale, long siret)
		{
			RaisonSociale = raisonSociale;
			SIRET = siret;
		}

		public override string ToString()
		{
			return $"""
				{base.ToString()}
				SIRET : {SIRET:### ### ### #####}
				""";
		}
	}
}
