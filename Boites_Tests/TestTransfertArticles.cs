namespace Boites_Tests;

[TestClass]
public class TestTransfertArticles
{
	[DataTestMethod]
	[DataRow(10, 14, 2, DisplayName = "2 articles transférés")]
	[DataRow(20, 10, 1, DisplayName = "1 seul article transféré")]
	[DataRow(25, 30, 0, DisplayName = "aucun article transféré")]
	public void TransférerArticlesentreBoites(double volArticle1, double volArticle2, int nbTransf)
	{
		Boite b1 = new Boite(3, 4, 5);
		Boite b2 = new Boite(2, 3, 4);
		Article a1 = new Article("Article1", volArticle1);
		Article a2 = new Article("Article2", volArticle2);

		b1.TryAddArticle(a1);
		b1.TryAddArticle(a2);
		int res = b1.TransfererContenuVers(b2);

		Assert.AreEqual(nbTransf, res, "Nombre articles transférés");
		Assert.AreEqual(2-nbTransf, b1.Articles.Count, "Articles restants dans boîte 1");
		Assert.AreEqual(nbTransf, b2.Articles.Count, "Articles dans boîte 2");
	}
}
