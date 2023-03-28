namespace Boites_Tests
{
	[TestClass]
	public class TestEtiquetage
	{
		[TestMethod]
		public void CréerBoiteSansMatiere()
		{
			Boite b = new Boite(2, 3, 4);

			Assert.AreEqual(24, b.Volume);
			Assert.AreEqual(Matieres.Carton, b.Matiere);
		}
		
		[TestMethod]
		public void CréerBoiteAvecMatiere()
		{
			Matieres mat = Matieres.Bois;
			Boite b = new Boite(3, 4, 5, mat);

			Assert.AreEqual(60, b.Volume);
			Assert.AreEqual(mat, b.Matiere);
		}
		
		[TestMethod]
		public void EtiqueterBoiteNonFragile()
		{
			Boite b = new Boite(3, 4, 5);
			Client client = new()
			{
				Numero = 123,
				Nom = "Dupont",
				Prenom = "Eric",
				Adresse = "3 rue Victor Hugo - 87000 Limoges"
			};
			long numColis = 78965445;

			b.Etiqueter(client, numColis);

			Assert.IsNotNull(b.EtiquetteColis);
			Assert.AreEqual(client.Numero, b.EtiquetteColis.Destinataire.Numero);
			Assert.AreEqual(numColis, b.EtiquetteColis.NumeroColis);
			Assert.AreEqual(Couleurs.Blanc, b.EtiquetteColis.Couleur);
			Assert.AreEqual(Formats.XL, b.EtiquetteColis.Format);
		}

		[TestMethod]
		public void EtiqueterBoiteFragile()
		{
			Boite b = new Boite(3, 4, 5);
			Client client = new()
			{
				Numero = 123,
				Nom = "Dupont",
				Prenom = "Eric",
				Adresse = "3 rue Victor Hugo - 87000 Limoges"
			};
			long numColis = 78965445;

			b.Etiqueter(client, numColis, true);

			Assert.IsTrue(b.Fragile);
		}
	}
}