namespace Boites;

public enum Couleurs { Blanc, Bleu, Vert, Jaune, Orange, Rouge, Marron }
public enum Formats { XS, S, M, L, XL }

public class Etiquette
{
	public required long NumeroColis { get; init; }
	public required Client Destinataire { get; init; }
	public required Couleurs Couleur { get; init; }
	public required Formats Format { get; init; }
}
