namespace SaisieAutomatisée;

public enum Status { Student, Employee, Unemployed, Retired }

public static class EnumExtensions
{
	public static string ToDisplayableString(this Status status)
	{
		string[] libellés = { "Etudiant", "Employé", "Sans emploi", "Retraité" };
		return libellés[(int)status];
	}
}
