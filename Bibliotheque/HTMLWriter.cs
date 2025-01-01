using System.Text;

namespace Bibliotheque;

public class HTMLWriter
{
	public static void GénérerPage(List<Livre> livres, string chemin)
	{
		using StreamWriter writer = new(chemin, false, Encoding.UTF8);

		writer.WriteLine("""
			<html lang="fr">
			<head>
			   <meta charset="UTF-8">
			   <meta http-equiv="X-UA-Compatible" content="IE=edge">
			   <meta name="viewport" content="width=device-width, initial-scale=1.0">
			   <title>Bibliothèque</title>
			</head>
			<body>
				<div style="display: grid; grid-template-columns: 200px 150px 1fr; grid-gap: .5rem; font-size:1.3rem;">
			""");

		foreach (Livre livre in livres)
		{
			writer.WriteLine($"""
				<img src="{livre.NomImage}" width="200px"/>
				<div style="font-weight: bold; text-align: right;">
				   <p>ISBN :</p>
				   <p>Titre :</p>
				   <p>Auteur :</p>
				   <p>Publication :</p>
				   <p>Description :</p>
				</div>
				<div>
					<p>{livre.ISBN}</p>
					<p>{livre.Titre}</p>
					<p>{livre.Auteur}</p>
					<p>{livre.Publication}</p>
					<p>{livre.Description}</p>
				</div>
				""");
		}

		writer.WriteLine("""
			   </div>
			</body>
			</html>
			""");
	}
}
