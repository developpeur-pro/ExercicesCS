namespace SaisieAutomatisée
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TesterSaisieCompte();
		}

		static void TesterSaisieCompte()
		{
			Console.WriteLine("Saisie d'un client\n");

			Client client = new();
			client.SaisirValeursPropriétés();
			//Input.SaisirValeursPropriétés(client);

			Console.WriteLine($"\nCompte créé : {client.Nom}, {client.Prenom}, {client.DateNais:d}");
		}
	}
}