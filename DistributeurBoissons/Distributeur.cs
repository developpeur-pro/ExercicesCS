namespace DistributeurBoissons;

public enum TypesBoissons { Café, Chocolat, Thé }

public class Boisson
{
	public TypesBoissons Type { get; set; }
	public int DoseSucre { get; set; }
}

public class Carte
{
	public string CodeDistributeur { get; }
	public int Id { get; }
	public decimal Solde { get; set; }

	public Carte(string codeDistributeur, int id)
	{
		CodeDistributeur = codeDistributeur;
		Id = id;
	}
}

public class Distributeur
{
	public static readonly decimal PRIX_BOISSON = 1m; // Prix des boissons
	public static readonly string CODE_DISTRI = "XYZ";
	public static readonly int CAFE = 0, CHOCOLAT = 1, THE = 2, SUCRE = 3, EAU = 4, GOBELETS = 5;
	
	private int[] _stocks = new int[6]; // Stocks de café, chocolat, thé, sucre, eau et gobelets

	/// <summary>
	/// Recharge le distributeur avec le nombre d'unités spécifié
	/// pour le stock d'indice spécifié ou pour tous les stocks si indice = -1
	/// </summary>
	/// <param name="nbUnits">Nombre d'unités de recharge</param>
	/// <param name="indiceStock">Indice du stock (optionnel)</param>
	/// <exception cref="ArgumentOutOfRangeException">Nombre d'unités en dehors de la plage [0, 100]
	/// ou indice de stock en dehors de la plage [-1, 5]</exception>
	public void Recharger(int nbUnits, int indiceStock = -1)
	{
		if (nbUnits < 0 || nbUnits > 100)
			throw new ArgumentOutOfRangeException("Le nombre d'unités doit être compris entre 0 et 100.");

		if (indiceStock < -1 || indiceStock > 5)
			throw new ArgumentOutOfRangeException("L'indice du stock doit être compris entre -1 et 5.");

		if (indiceStock >= 0)
		{
			_stocks[indiceStock] = nbUnits;
		}
		else
		{
			for (int i = 0; i < _stocks.Length; i++)
				_stocks[i] = nbUnits;	
		}		
	}

	/// <summary>
	/// Commande une boisson au distributeur
	/// </summary>
	/// <param name="carte">carte utilisée pour payer</param>
	/// <param name="type">type de boisson</param>
	/// <param name="doseSucre">dose de sucre</param>
	/// <returns>Boisson commandée</returns>
	public Boisson CommanderBoisson(Carte carte, TypesBoissons type, int doseSucre)
	{
		ValiderCarte(carte);
		VérifierStocks(type);
		DébiterCarte(carte, PRIX_BOISSON);
		Boisson boisson = PreparerBoisson(type, doseSucre);
		return boisson;
	}

	// Vérifie si le code distributeur de la carte est le bon
	// et émet une exception si ce n'est pas le cas
	private void ValiderCarte(Carte carte)
	{
		if (carte.CodeDistributeur != CODE_DISTRI)
			throw new UnauthorizedAccessException("Carte non reconnue.");
	}

	// Vérifie que les stocks sont suffisants pour préparer la boisson demandée
	// et émet une exception si ce n'est pas le cas
	private void VérifierStocks(TypesBoissons typeBoisson)
	{
		if (_stocks[(int)typeBoisson] == 0)
			throw new InvalidOperationException($"Stock de {typeBoisson} insufisant.");
		else if (_stocks[EAU] == 0)
			throw new InvalidOperationException($"Stock d'eau insufisant.");
		else if (_stocks[GOBELETS] == 0)
			throw new InvalidOperationException($"Stock de gobelets insufisant.");
	}

	// Débite la carte du montant demandé
	// ou émet une exception si le solde est insuffisant
	private void DébiterCarte(Carte carte, decimal montant)
	{
		if (carte.Solde - montant < 0)
			throw new ArgumentException("Solde de carte insuffisant.");

		carte.Solde -= montant;
	}

	// Prépare et renvoie la boisson demandée
	private Boisson PreparerBoisson(TypesBoissons type, int doseSucre = 3)
	{
		// Décrémente les stocks de boisoon, sucre, eau et gobelets
		_stocks[(int)type]--;

		if (doseSucre > _stocks[SUCRE])
		{
			doseSucre = _stocks[SUCRE];
			_stocks[SUCRE] = 0;
		}
		else
			_stocks[SUCRE] -= doseSucre;

		_stocks[EAU]--;
		_stocks[GOBELETS]--;

		// Prépare la boisson
		Boisson b = new() { Type = type, DoseSucre = doseSucre };
		return b;
	}
}