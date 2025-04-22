namespace ClassificationPériodique;

public record class Famille
{
	public required short Id { get; init; }
	public required string Nom { get; init; }
	public string? Couleur { get; init; }
}

public record class Etat
{
	public required char Code { get; init; }
	public required string Nom { get; init; }
	public string? Couleur { get; init; }
}

public record class Element
{
	public required short NumAtomique { get; init; }
	public required string Symbole { get; init; }
	public required string Nom { get; init; }
	public required char CodeEtat { get; init; }
	public short Période { get; init; }
	public short NumFamille { get; init; }
}