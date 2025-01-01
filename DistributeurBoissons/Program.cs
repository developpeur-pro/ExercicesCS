using System.Text;

namespace DistributeurBoissons;

internal class Program
{
	static void Main(string[] args)
	{
		Console.OutputEncoding = Encoding.Unicode;
		try
		{
			Carte carte = new("XYZ", 1) { Solde = 12m };
			Distributeur distri = new();
			distri.Recharger(5);

			int numCmd = 1;
			while (numCmd <= 15)
			{
				try
				{
					Boisson b = distri.CommanderBoisson(carte, TypesBoissons.Thé, 2);
					Console.WriteLine($"{b.Type} N°{numCmd} sucré à {b.DoseSucre} servi. Solde de carte : {carte.Solde:C0}");
					numCmd++;
				}
				catch (InvalidOperationException e)
				{
					Console.WriteLine($"Boisson N°{numCmd} non servie. {e.Message} Rechargement de la machine.");
					distri.Recharger(5);
				}
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}			
	}
}