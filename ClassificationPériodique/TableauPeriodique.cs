using System.Text.RegularExpressions;

namespace ClassificationPériodique;

public static class TableauPeriodique
{
	public static readonly string NomFichierTxt = "TableauPeriodique.txt";
	public static readonly string NomFichierHtml = "TableauPeriodique.html";
	public static readonly Dictionary<int, Famille> Familles = new();
	public static readonly Dictionary<char, Etat> Etats = new();
	public static readonly Dictionary<short, Element> Elements = new();

	#region Partie 1
	// Charge le contenu du fichier txt dans les dictionnaires
	public static void ChargerFichierTxt()
	{
		string[] lignes = File.ReadAllLines(NomFichierTxt);

		// Charge les familles
		for (short i = 1; i < 12; i++)
		{
			Famille famille = new Famille
			{
				Id = short.Parse(lignes[i][0..2]),
				Nom = lignes[i][3..]
			};

			Familles.Add(famille.Id, famille);
		}

		// Charge les états
		for (short i = 14; i < 18; i++)
		{
			Etat état = new Etat
			{
				Code = lignes[i][0],
				Nom = lignes[i][2..]
			};

			Etats.Add(état.Code, état);
		}

		// Charge les éléments
		for (int i = 20; i < lignes.Length; i++)
		{
			Element e = new Element
			{
				Période = short.Parse(lignes[i][0..1]),
				NumAtomique = short.Parse(lignes[i][2..5]),
				Symbole = lignes[i][6..8],
				CodeEtat = lignes[i][9],
				NumFamille = short.Parse(lignes[i][11..13]),
				Nom = lignes[i][14..]
			};

			Elements.Add(e.NumAtomique, e);
		}
	}

	// Renvoie une chaîne donnant le détail de l'élément
	// dont le numéro est passé en paramètre
	public static string GetDetailElement(short numéro)
	{
		Element el = Elements[numéro];

		string desc = $"""
			Numéro atomique : {el.NumAtomique}
			Symbole : {el.Symbole}
			Nom : {el.Nom}
			Période : {el.Période}
			Famille : {Familles[el.NumFamille].Nom}
			Etat naturel : {Etats[el.CodeEtat].Nom}
			""";

		return desc;
	}

	// Associe une couleur au code d'état naturel passé en paramètre
	public static ConsoleColor GetCouleurEtat(char etat) => etat switch
	{
		'S' => ConsoleColor.White,
		'L' => ConsoleColor.Blue,
		'G' => ConsoleColor.Red,
		_ => ConsoleColor.DarkGray
	};

	#endregion
	#region Partie 2

	// Charge le contenu du fichier HTML dans les dictionnaires
	public static void ChargerFichierHtml()
	{
		string txt = File.ReadAllText(NomFichierHtml);

		// Charge les familles
		string modèle = @"<td bgcolor=""(#\w+)"" align=""center"">([\w -]+)</td>";
		short f = 0;
		foreach (Match match in Regex.Matches(txt, modèle, RegexOptions.IgnoreCase))
		{
			Famille famille = new Famille
			{
				Id = ++f,
				Nom = match.Groups[2].Value,
				Couleur = match.Groups[1].Value
			};

			Familles.Add(famille.Id, famille);
		}

		// Charge les états
		modèle = @"<span style=""color:(\w+)"">(\w+)</span>";
		foreach (Match match in Regex.Matches(txt, modèle, RegexOptions.IgnoreCase))
		{
			Etat état = new Etat
			{
				Code = match.Groups[2].Value[0],
				Nom = match.Groups[2].Value,
				Couleur = match.Groups[1].Value
			};

			Etats.Add(état.Code, état);
		}

		// Charge les éléments
		modèle = @"<td style="".*background-color:(#\w+);.*;color:(\w+);"">\s*(\d+)\s*<br><big><b><a href=.*\s+title=""(\w+)"">\s*(\w+)\s*</a></b></big>\s*</td>";

		foreach (Match match in Regex.Matches(txt, modèle, RegexOptions.IgnoreCase))
		{
			Element e = new Element
			{
				NumFamille = GetIdFamille(match.Groups[1].Value),
				CodeEtat = GetCodeEtat(match.Groups[2].Value),
				NumAtomique = short.Parse(match.Groups[3].Value),
				Nom = match.Groups[4].Value,
				Symbole = match.Groups[5].Value
			};

			Elements.Add(e.NumAtomique, e);
		}
	}
	
	private static char GetCodeEtat(string couleur) => couleur switch
	{
		"black" => 'S',
		"blue" => 'L',
		"red" => 'G',
		_ => 'I'
	};

	private static short GetIdFamille(string couleur) => couleur switch
	{
		"#F66" => 1,
		"#F6CFA1" => 2,
		"#FFBFFF" => 3,
		"#FF99CC" => 4,
		"#FFC0C0" => 5,
		"#CCC" => 6,
		"#CC9" => 7,
		"#A0FFA0" => 8,
		"#FF9" => 9,
		"#C0E8FF" => 10,
		_ => 11
	};

	// Génère une version épurée du fichier HTML
	public static void TransformerFichier()
	{
		string txt = File.ReadAllText(NomFichierHtml);

		// Génère un fichier sans les styles des cellules d'éléments hormis la couleur de fond
		string modèle = @"<td style=""[^"">]*(?<bgcolor>background-color:#\w+)[^"">]*;"">";
		string nouveauTexte = Regex.Replace(txt, modèle, @"<td style=""${bgcolor};"">");

		File.WriteAllText("TableauEpuré.html", nouveauTexte);
	}
	#endregion
}
