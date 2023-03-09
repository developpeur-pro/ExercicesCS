using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
	public enum Matieres { Carton, Plastique, Bois, Metal }
	
	internal class Boite
	{
		#region Constructeurs
		public Boite(double hauteur, double largeur, double longeur)
		{
			Hauteur = hauteur;
			Largeur = largeur;
			Longeur = longeur;
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
		#endregion
	}
}
