using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
	public enum Matieres { Carton, Plastique, Bois, Metal }

	internal class Boite
	{
		private List<Article> _articles;

		#region Constructeurs
		public Boite(double hauteur, double largeur, double longeur)
		{
			Hauteur = hauteur;
			Largeur = largeur;
			Longeur = longeur;
			_articles = new List<Article>();
			NbBoites++;
		}

		public Boite(double hauteur, double largeur, double longeur, Matieres matiere) :
			this(hauteur, largeur, longeur)
		{
			Matiere = matiere;
		}
		#endregion

		#region Propriétés
		public double Hauteur { get; set; }
		public double Largeur { get; } = 30;
		public double Longeur { get; } = 30;
		public Matieres Matiere { get; } = Matieres.Carton;
		public double Volume => Hauteur * Largeur * Longeur;

		public Etiquette? EtiquetteColis { get; private set; }
		public bool Fragile { get; private set; }
		public ReadOnlyCollection<Article> Articles => _articles.AsReadOnly();
		public string Description
		{
			get
			{
				string desc = $"Boîte de volume {Volume} en {Matiere} contenant :\n";
				foreach (Article article in _articles)
				{
					desc += $" - {article.Libelle}\n";
				}
				return desc;
			}
		}

		public static int NbBoites { get; private set; }
		#endregion

		#region Méthodes publiques
		public void Etiqueter(Client dest, long numColis)
		{
			EtiquetteColis = new Etiquette
			{
				Destinataire = dest,
				NumeroColis = numColis,
				Couleur = Couleurs.Blanc,
				Format = Formats.XL
			};
		}

		public void Etiqueter(Client dest, long numColis, bool f)
		{
			Etiqueter(dest, numColis);
			Fragile = f;
		}

		public static bool Comparer(Boite b1, Boite b2)
		{
			return (b1.Hauteur == b2.Hauteur && b1.Largeur == b2.Largeur && b1.Longeur == b2.Longeur && b1.Matiere == b2.Matiere);
		}

		public bool Comparer(Boite b1)
		{
			return Comparer(b1, this);
		}

		/// <summary>
		/// Tente d'ajouter un article dans la boîte si la place restante le permet
		/// </summary>
		/// <param name="article">article à ajouter</param>
		/// <returns>True si l'article a été ajouté, false sinon</returns>
		public bool TryAddArticle(Article article)
		{
			double volumeOccupe = 0;
			foreach (Article a in _articles)
			{
				volumeOccupe += a.Volume;
			}

			if (volumeOccupe + article.Volume <= Volume)
			{
				_articles.Add(article);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Transfère les articles de la boîte courante vers la boîte passée en paramètre
		/// Seuls les articles qui tiennent dans la boîte de destination sont transférés
		/// </summary>
		/// <param name="b">Boîte de destination</param>
		/// <returns>Nombre d'articles transférés</returns>
		public int TransfererContenuVers(Boite b)
		{
			int nbArticlesTransf = 0;

			for (int i = _articles.Count - 1; i >= 0; i--)
			{
				if (b.TryAddArticle(_articles[i]))
				{
					_articles.RemoveAt(i);
					nbArticlesTransf++;
				}
			}

			return nbArticlesTransf;
		}
		#endregion
	}
}
