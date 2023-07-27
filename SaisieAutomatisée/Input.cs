using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SaisieAutomatisée
{
	public static class Input
	{
		// Fait saisir les valeurs des propriétés de l'entité en gérant les erreurs de validation
		public static void SaisirValeursPropriétés<T>(T entity) where T : class
		{
			PropertyInfo[] props = entity.GetType().GetProperties();
			foreach (PropertyInfo p in props)
			{
				string prompt = p.GetCustomAttribute<DisplayAttribute>()?.GetPrompt() ?? p.Name;

				bool ok;
				do
				{
					ok = false;
					Console.WriteLine($"\n{prompt}:");
					string rep = Console.ReadLine() ?? "";
					try
					{
						// Convertit la valeur saisie dans le type de la propriété
						object val = Convert.ChangeType(rep, p.PropertyType);
						// Affecte cette valeur à la propriété
						p.SetValue(entity, val);
						ok = true;
					}
					catch (OverflowException)
					{
						Console.WriteLine("Le nombre saisi est trop grand");
					}
					catch (FormatException)
					{
						Console.WriteLine("Format de donnée non valide");
					}
					catch (TargetInvocationException tie)
					{
						Console.WriteLine(tie.InnerException?.Message);
					}
				} while (!ok);
			}
		}
	}
}
