namespace Boites_Tests;

[TestClass]
public class TestAjoutArticles
{
	[TestMethod]
	public void Ajouter1ArticleDansBoiteAssezGrande()
	{
		Boite b = new Boite(3, 4, 5);
		Article a = new Article("lot de 6 assiettes plates", 40);

		bool res = b.TryAddArticle(a);
		Assert.IsTrue(res);
		Assert.AreEqual(1, b.Articles.Count);
	}
	
	[TestMethod]
	public void Ajouter2ArticlesDansBoiteAssezGrande()
	{
		Boite b = new Boite(3, 4, 5);
		Article a1 = new Article("lot de 6 assiettes plates", 40);
		Article a2 = new Article("lot de 12 couverts", 20);

		bool res = b.TryAddArticle(a1);
		Assert.IsTrue(res);
		res = b.TryAddArticle(a2);
		Assert.IsTrue(res);

		Assert.AreEqual(2, b.Articles.Count);
	}

	[TestMethod]
	public void Ajouter1ArticleDansBoiteTropPetite()
	{
		Boite b = new Boite(2, 3, 4);
		Article a = new Article("lot de 6 assiettes plates", 40);

		bool res = b.TryAddArticle(a);
		Assert.IsFalse(res);
		Assert.AreEqual(0, b.Articles.Count);
	}

	[TestMethod]
	public void Ajouter2ArticlesDansBoiteTropPetite()
	{
		Boite b = new Boite(2, 3, 4);
		Article a1 = new Article("lot de 12 couverts", 20);
		Article a2 = new Article("lot de 6 assiettes plates", 40);

		bool res = b.TryAddArticle(a1);
		Assert.IsTrue(res);
		res = b.TryAddArticle(a2);
		Assert.IsFalse(res);

		Assert.AreEqual(1, b.Articles.Count);
	}
}
