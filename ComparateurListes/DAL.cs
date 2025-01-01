namespace ComparateurListes;

public record class Etudiant(string Nom, string Prénom)
{
	public override string ToString()
	{
		return Nom + " " + Prénom;
	}
}

/*	public class Etudiant
	{
		public Etudiant(string nom, string prénom)
		{
			Nom = nom;
			Prénom = prénom;
		}

		public string Nom { get; }
		public string Prénom { get; }

		public override string ToString()
		{
			return Nom + " " + Prénom;
		}
	}*/

public class DAL
{
	public static HashSet<Etudiant> GetEtudiants(string nomFichier)
	{
		HashSet<Etudiant> étudiants = new();
		string[] lignes = File.ReadAllLines(nomFichier);

		for (int l = 1; l < lignes.Length; l++)
		{
			string[] infos = lignes[l].Split(';');
			étudiants.Add(new Etudiant(infos[0], infos[1]));
		}

		return étudiants;
	}
}
