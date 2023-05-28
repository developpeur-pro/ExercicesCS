namespace GestionStock
{
	public enum TypesMouvements { Entrée, Sortie, RAZ }

	public class Stock
	{
		private SortedList<DateOnly, Mouvement> _mouvements = new();

		public decimal SeuilAlerte { get; set; }
		public event EventHandler<(DateOnly, decimal)>? AlerteStockBas;

		// Ajoute la quantité spécifiée au stock, à la date spécifiée
		public void Ajouter(DateOnly date, decimal quantité)
		{
			_mouvements.Add(date, new Mouvement(TypesMouvements.Entrée, date, quantité));
		}

		// Retire la quantité spécifiée du stock à la date spécifiée
		// Lève une InvalidOperationException si la quantité en stock est insuffisante
		public void Retirer(DateOnly date, decimal quantité)
		{
			// On vérifie si la quantité en stock est suffisante
			// Si ce n'est pas le cas, on lève une exception
			decimal etatStock = GetEtatStock(date);
			if (quantité <= etatStock)
				_mouvements.Add(date, new Mouvement(TypesMouvements.Sortie, date, quantité));
			else
				throw new InvalidOperationException($"Quantité en stock insuffisante ({etatStock})");

			// Si la quantité en stock devient inférieure au seuil défini, on émet un évènement
			if (etatStock < SeuilAlerte)
				AlerteStockBas?.Invoke(this, (date, etatStock));
		}

		// Remet le stock à zéro à la date spécifiée
		public void RemettreAZéro(DateOnly date)
		{
			_mouvements.Add(date, new Mouvement(TypesMouvements.RAZ, date, 0));
		}

		// Obtient l'état du stock à une date donnée
		public decimal GetEtatStock(DateOnly date)
		{
			decimal qte = 0m;

			foreach (var mvt in _mouvements)
			{
				if (mvt.Value.DateMvt > date) break;

				switch (mvt.Value.Type)
				{
					case TypesMouvements.Entrée:
						qte += mvt.Value.Quantité;
						break;
					case TypesMouvements.Sortie:
						qte -= mvt.Value.Quantité;
						break;
					default:
						qte = 0;
						break;
				}
			}

			return qte;
		}
	}

	// Décrit un mouvement de stock
	public record class Mouvement(TypesMouvements Type, DateOnly DateMvt, decimal Quantité);
}
