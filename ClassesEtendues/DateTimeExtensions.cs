namespace SaisieAutomatisée
{
	public static class DateTimeExtensions
	{
		// Renvoie une date correspondant au premier jour du mois de la date
		public static DateTime BeginningOfTheMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		// Renvoie une date correspondant au dernier jour du mois de la date
		public static DateTime EndOfTheMonth(this DateTime date)
		{
			var endOfTheMonth = new DateTime(date.Year, date.Month, 1)
				.AddMonths(1)
				.AddDays(-1);

			return endOfTheMonth;
		}

		// Renvoie le nombre d'années entières écoulées entre la date et la date du jour
		public static int GetAge(this DateTime date)
		{
			int ans = DateTime.Today.Year - date.Year;
			if (DateTime.Today.Month < date.Month ||
				(DateTime.Today.Month == date.Month && DateTime.Today.Day < date.Day))
				ans--;

			return ans;
		}

		// renvoie la date additionnée d’un nombre de jours travaillés, c'est-à-dire hors weekend 
		public static DateTime AddWorkedDays(this DateTime date, int days)
		{
			// incrément
			int inc = days < 0 ? -1 : 1;

			// Si la date est un jour de weekend,
			// on se positionne sur le dernier ou prochain jour travaillé
			DateTime dt = date;
			while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
			{
				dt = dt.AddDays(inc);
			}

			// On incrémente ensuite la date de jour en jour jusqu'à avoir ajouté
			// le nombre de jours demandé sans compter les samedi et dimanche
			int cpt = 0;
			while (cpt < days * inc)
			{
				dt = dt.AddDays(inc);
				if (dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday)
					cpt++;
			}

			return dt;
		}
	}
}
