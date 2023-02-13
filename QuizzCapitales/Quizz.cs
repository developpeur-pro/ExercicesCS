using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzCapitales
{
	internal class Quizz
	{
		static string[] pays = { "Albanie", "Allemagne", "Andorre", "Autriche", "Belgique",
								"Biélorussie", "Bosnie-Herzégovine", "Bulgarie", "Chypre", "Croatie" };

		static string[] capitales =  { "Tirana", "Berlin", "Andorre-la-Vieille", "Vienne", "Bruxelles",
								"Minsk", "Sarajevo", "Sofia", "Nicosie",  "Zagreb" };

		public static void Jouer()
		{
			bool continuer = true;
			while (continuer)
			{
				int bonnesRep = 0;
				for (int i = pays.Length - 1; i >= 0; i--)
				{
					if (PoserQuestion(i)) bonnesRep++;
				}
				Console.WriteLine($"\n{bonnesRep} bonnes réponses");

				continuer = DemanderSiRejouer();
			}
		}

		public static void Jouer(params int[] numQuestions)
		{
			bool continuer = true;
			while (continuer)
			{
				int bonnesRep = 0;
				foreach (int num in numQuestions)
				{
					if (num > 0 && num <= pays.Length && PoserQuestion(num - 1)) bonnesRep++;
				}
				Console.WriteLine($"\n{bonnesRep} bonnes réponses");

				continuer = DemanderSiRejouer();
			}
		}

		public static (int, int, int) Générer3Numéros()
		{
			(int n1, int n2, int n3) numéros;
			Random rand = new Random(); // Initialise le générateur
			numéros.n1 = rand.Next(1, 11); // Génère un entier compris entre 1 et 10
			numéros.n2 = rand.Next(1, 11);
			numéros.n3 = rand.Next(1, 11);

			return numéros;
		}

		public static (int, int, int) Saisir3Numéros()
		{
			(int n1, int n2, int n3) numéros;
			numéros.n1 = SaisirNombre(1, 10);
			numéros.n2 = SaisirNombre(1, 10);
			numéros.n3 = SaisirNombre(1, 10);

			return numéros;
		}

		static int SaisirNombre(int min, int max)
		{
			Console.WriteLine($"Saisissez un nombre compris entre {min} et {max} :");
			bool repOk;
			int num;
			do
			{
				string? rep = Console.ReadLine();
				repOk = int.TryParse(rep, out num) && num >= min && num <= max;

			} while (!repOk);

			return num;
		}

		static bool PoserQuestion(int numQuestion)
		{
			Console.WriteLine($"\nQuelle est la capitale du pays suivant : {pays[numQuestion]} ?");
			string? rep = Console.ReadLine();
			if (rep == capitales[numQuestion])
			{
				Console.WriteLine("Bravo !");
				return true;
			}
			else
			{
				Console.WriteLine($"Mauvaise réponse. La réponse était : {capitales[numQuestion]}");
				return false;
			}
		}

		static bool DemanderSiRejouer()
		{
			Console.WriteLine("Voulez-vous rejouer (O/N) ?");
			string? rep2 = Console.ReadLine();
			if (rep2 == "O" || rep2 == "o")
			{
				Console.Clear();
				return true;
			}
			else
			{
				Console.WriteLine("Merci d'avoir joué");
				return false;
			}
		}
	}
}
