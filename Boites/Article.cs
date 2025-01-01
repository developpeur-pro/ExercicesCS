namespace Boites;

public class Article
{
	public string Libelle { get; }
	public double Volume { get; }

	public Article(string libelle, double volume)
	{
		Libelle = libelle;
		Volume = volume;
	}

}
