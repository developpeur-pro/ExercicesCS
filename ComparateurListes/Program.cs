namespace ComparateurListes
{
	internal class Program
	{
		static void Main(string[] args)
		{
			HashSet<Etudiant> ens1 = DAL.GetEtudiants("Etudiants1.csv");
			HashSet<Etudiant> ens2 = DAL.GetEtudiants("Etudiants2.csv");

			HashSet<Etudiant> exclusifs1 = new(ens1);
			exclusifs1.ExceptWith(ens2);

			Console.WriteLine("Etudiants présents uniquement dans le 1er fichier :\n");
			foreach (Etudiant e in exclusifs1) Console.WriteLine(e);

			HashSet<Etudiant> exclusifs2 = new(ens2);
			exclusifs2.ExceptWith(ens1);

			Console.WriteLine("\nEtudiants présents uniquement dans le 2ème fichier :\n");
			foreach (Etudiant e in exclusifs2) Console.WriteLine(e);
		}
	}
}