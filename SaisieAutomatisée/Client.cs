using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SaisieAutomatisée
{
	public abstract class ValidationBase
	{
		/// <summary>
		/// Vérifie la validité d'une valeur de propriété à partir des attributs qui la décorent
		/// </summary>
		/// <param name="value">Valeur à valider</param>
		/// <param name="propertyName">Nom de la propriété si pas déterminé automatiquement</param>
		/// <exception cref="ValidationException">valeur de propriété non valide</exception>
		public void ValidateProperty(object value, [CallerMemberName] string? propertyName = null)
		{
			ValidationContext context = new(this);
			context.MemberName = propertyName;
			Validator.ValidateProperty(value, context);
			// La ligne précédente lève une ValidationException si la valeur n'est pas valide
		}
	}

	public class Client : ValidationBase
	{
		private string _nom = string.Empty;
		private string _prenom = string.Empty;
		private int _id;
		private DateTime _dateNais;

		[Display(Prompt = "Numéro")]
		[Required(ErrorMessage = "Le numéro de client est obligatoire")]
		[Range(1, 99999, ErrorMessage = "Le numéro doit être compris entre 1 et 99999")]
		public int Id
		{
			get { return _id; }
			set
			{
				ValidateProperty(value);
				_id = value;
			}
		}

		[Display(Prompt = "Nom")]
		[Required(ErrorMessage = "Le nom est obligatoire")]
		[MaxLength(40, ErrorMessage = "le nom ne doit pas dépasser 40 caractères")]
		public string Nom
		{
			get => _nom;
			set
			{
				ValidateProperty(value);
				_nom = value;
			}
		}

		[Display(Prompt = "Prénom")]
		[Required(ErrorMessage = "Le prénom est obligatoire")]
		[MaxLength(40, ErrorMessage = "le prénom ne doit pas dépasser 40 caractères")]
		public string Prenom
		{
			get => _prenom;
			set
			{
				ValidateProperty(value);
				_prenom = value;
			}
		}

		[Display(Prompt = "Date de naissance")]
		[CustomValidation(typeof(ValidationHelper), nameof(ValidationHelper.CheckBirthDate))]
		public DateTime DateNais
		{
			get => _dateNais;
			set
			{
				ValidateProperty(value);
				_dateNais = value;
			}
		}
	}

	public static class ValidationHelper
	{
		public static ValidationResult? CheckBirthDate(DateTime dt)
		{
			if (dt < new DateTime(1900, 1, 1) || dt > DateTime.Today)
				return new ValidationResult($"La date de naissance doit être comprise entre le 01/01/1900 et le {DateTime.Today:d}");

			return ValidationResult.Success;
		}
	}
}
