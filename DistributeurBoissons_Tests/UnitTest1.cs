using DistributeurBoissons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace DistributeurBoissons_Tests
{
	[TestClass]
	public class TestDistributeur
	{
		private Distributeur _distri;
		private Carte _carte;
		
		[TestInitialize]
		public void InitialiserTest()
		{
			_distri = new Distributeur();	// Distributeur vide
			_carte = new Carte("XYZ", 1); // Carte valide vide
		}

		[DataTestMethod]
		[DataRow(-1, -1, DisplayName = "Nb unit�s < 0")]
		[DataRow(101, -1, DisplayName = "Nb unit�s > 100")]
		[DataRow(100, -2, DisplayName = "indice de stock < -1")]
		[DataRow(100, 6, DisplayName = "indice de stock > 5")]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void RechargerDistriDeFa�onIncorrect(int nbUnits, int indideStock)
		{
			_distri.Recharger(nbUnits, indideStock);
		}

		[TestMethod]
		public void CommandeOk()
		{
			TypesBoissons type = TypesBoissons.Chocolat;
			int doseSucre = 1;
			_carte.Solde = 5m;

			_distri.Recharger(10);
			Boisson b = _distri.CommanderBoisson(_carte, type, doseSucre);

			Assert.AreEqual(type, b.Type, "type de boisson");
			Assert.AreEqual(doseSucre, b.DoseSucre, "dose de sucre");
		}

		[TestMethod]
		public void CommandeOkMaisSucreInsuffisant()
		{
			TypesBoissons type = TypesBoissons.Chocolat;
			int doseSucre = 6;	// Nb d'unit�s de sucre demand�
			int unit�sSucreDistri = 5; // Nb d'unit�s de sucre restantes dans le distributeur
			_carte.Solde = 5m;

			_distri.Recharger(10);
			_distri.Recharger(unit�sSucreDistri, 3);
			Boisson b = _distri.CommanderBoisson(_carte, type, doseSucre);

			Assert.AreEqual(type, b.Type, "type de boisson");
			Assert.AreEqual(unit�sSucreDistri, b.DoseSucre, "dose de sucre");
		}

		[TestMethod]
		[ExpectedException(typeof(UnauthorizedAccessException))]
		public void CommandeAvecCarteNonValide()
		{
			Carte carte = new Carte("ABC", 0) { Solde = 3 };
			_distri.CommanderBoisson(carte, TypesBoissons.Th�, 2);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CommandeAvecSoldeCarteInsuffisant()
		{
			_carte.Solde = 0m;
			_distri.Recharger(10);
			_distri.CommanderBoisson(_carte, TypesBoissons.Caf�, 0);
		}

		[DataTestMethod]
		[DataRow(0, DisplayName = "Stock de caf� vide")]
		[DataRow(1, DisplayName = "Stock de chocolat vide")]
		[DataRow(2, DisplayName = "Stock de th� vide")]
		[DataRow(4, DisplayName = "Stock d'eau vide")]
		[DataRow(5, DisplayName = "Stock de gobelets vide")]
		[ExpectedException(typeof(InvalidOperationException))]
		public void CommandeAvecStockInsuffisant(int indiceStock)
		{
			_distri.Recharger(1);	// Initialise tous les stocks � 1
			_distri.Recharger(0, indiceStock); // Vide le stock d'indice donn�
			TypesBoissons typeBoisson = TypesBoissons.Caf�;
			if (indiceStock < 3)
				typeBoisson = (TypesBoissons)indiceStock;
			int doseSucre = 0;

			_distri.CommanderBoisson(_carte, typeBoisson, doseSucre);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void CommandeOkPuisStockInsufisant()
		{
			TypesBoissons typeBoisson = TypesBoissons.Th�;
			int doseSucre = 3;

			_distri.Recharger(2);
			_distri.Recharger(1,2);
			_carte.Solde = 2m;
			Boisson b = _distri.CommanderBoisson(_carte, typeBoisson, doseSucre);

			Assert.AreEqual(typeBoisson, b.Type, "type de boisson");
			Assert.AreEqual(2, b.DoseSucre, "dose de sucre");

			// Nouvelle commande du m�me type de boisson
			_distri.CommanderBoisson(_carte, typeBoisson, doseSucre);
		}
	}
}