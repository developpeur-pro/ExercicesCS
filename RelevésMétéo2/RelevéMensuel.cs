namespace RelevésMétéo2;

public class RelevéMensuel
{
	public int Mois { get; init; }
	public int Année { get; init; }
	public float Tmin { get; init; }
	public float Tmax { get; init; }
	public float Tmoy { get; init; }
	public float Vent { get; init; }
	public float Soleil { get; init; }
	public float Pluie { get; init; }

	public override string ToString()
	{
		return string.Format($"{Mois:00}/{Année} | {Tmin,9:N1} | {Tmax,9:N1} | {Tmoy,9:N1} | " +
			$"{Vent,11:N1} | {Soleil,9:N1} | {Pluie,11:N1}");
	}

	public const string EnTeteTableau = """
		Mois    | Tmin (°C) | Tmax (°C) | Tmoy (°C) | Vent (km/h) | Soleil (H) | Pluie (mm)
		-----------------------------------------------------------------------------------
		""";
}