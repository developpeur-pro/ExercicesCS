using System.Runtime.CompilerServices;

namespace Boites
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Création de boîtes
			Boite b1 = new Boite(20, 20, 10);
			Console.WriteLine($"Boite de volume {b1.Volume} en {b1.Matiere}");
			Console.WriteLine($"Nombre de boites : {Boite.NbBoites}");

			Boite b2 = new Boite(20, 30, 10, Matieres.Bois);
			Console.WriteLine($"Boite de volume {b2.Volume} en {b2.Matiere}");
			Console.WriteLine($"Nombre de boites : {Boite.NbBoites}");

			// Comparaison de boîtes
			Console.WriteLine($"Boites identiques : {b1.Comparer(b2)}");

			Boite b3 = new Boite(20, 30, 10, Matieres.Carton);
			Console.WriteLine($"Boites identiques : {b2.Comparer(b3)}");
			Console.WriteLine();

			#region Composition et agrégation

			// Etiquetage d'une boîte sans classe Client
			/*b1.Etiqueter("M. Dupont Eric, 3 rue Victor Hugo - 87000 Limoges", 123456789, false);

			if (b1.EtiquetteColis != null)
			{
				Console.WriteLine($"""
					Colis N° {b1.EtiquetteColis.NumeroColis}
					Destinataire : {b1.EtiquetteColis.Destinataire}
					{(b1.Fragile ? "Fragile" : "Non fragile")}
					""");
			}*/

			// Etiquetage d'une boîte avec classe Client

			Client cli = new Client
			{
				Numero = 123,
				Nom = "Dupont",
				Prenom = "Eric",
				Adresse = "3 rue Victor Hugo - 87000 Limoges"
			};

			b2.Etiqueter(cli, 123456789, false);

			if (b2.EtiquetteColis != null)
			{
				Client c = b2.EtiquetteColis.Destinataire;

				Console.WriteLine($"""
					Colis N° {b2.EtiquetteColis.NumeroColis}
					Destinataire : {c.Nom} {c.Prenom}, {c.Adresse}
					{(b2.Fragile ? "Fragile" : "Non fragile")}
					""");
			}
			#endregion

			#region Listes
			Console.WriteLine();
			bool res = b3.TryAddArticle(new Article("lot de 6 assiettes plates", 4000));
			Console.WriteLine(b3.Description);
			res = b3.TryAddArticle(new Article("lot de 12 couverts", 1000));
			Console.WriteLine(b3.Description);
			res = b3.TryAddArticle(new Article("lot de 6 verres", 2000));
			Console.WriteLine(b3.Description);

			// Transfert du contenu de la boîte b3 vers la boîte vide b2 de même volume
			int nbArticlesTransf = b3.TransfererContenuVers(b2);
			Console.WriteLine($"{nbArticlesTransf} articles transférés");
			Console.WriteLine(b3.Description);
			Console.WriteLine(b2.Description);

			// Transfert du contenu de la boîte b2 vers la boîte vide b1 trop petite
			nbArticlesTransf = b2.TransfererContenuVers(b1);
			Console.WriteLine($"{nbArticlesTransf} articles transférés");
			Console.WriteLine(b2.Description);
			Console.WriteLine(b1.Description);

			#endregion
		}
	}
}