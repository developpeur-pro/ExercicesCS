namespace ClassificationPériodique;

internal class Program
{
	static void Main(string[] args)
	{
		//TableauPeriodique.TransformerFichier();		
		//TableauPeriodique.ChargerFichierHtml();
		TableauPeriodique.ChargerFichierTxt();

		// Excécute l'appli en boucle jusqu'à l'appui sur Echap
		while (true)
		{
			short num = SaisirNuméro();

			// Affiche le détail de l'élément avec la couleur appropriée
			ConsoleColor couleurOrig = Console.ForegroundColor;
			Console.ForegroundColor = TableauPeriodique.GetCouleurEtat(TableauPeriodique.Elements[num].CodeEtat);

			Console.WriteLine(TableauPeriodique.GetDetailElement(num));
			Console.ForegroundColor = couleurOrig;

			// Attend l'appui sur Echap pour vider l'écran et continuer la boucle
			Console.WriteLine("\nAppuyez sur Echap pour quitter ou n'importe quelle autre touche pour continuer.");
			if (Console.ReadKey(true).Key == ConsoleKey.Escape)
				Environment.Exit(0);
			else
				Console.Clear();
		}
	}

	// Fait saisir un numéro d'élément correct à l'utilisateur et le renvoie
	public static short SaisirNuméro()
	{
		bool saisieOK = false;
		short num = 1;
		while (!saisieOK)
		{
			Console.WriteLine("Numéro atomique de l'élément :");
			string? rep = Console.ReadLine();
			saisieOK = short.TryParse(rep, out num) && num >= 1 && num <= 118;
		}
		Console.Clear();
		return num;
	}
}
