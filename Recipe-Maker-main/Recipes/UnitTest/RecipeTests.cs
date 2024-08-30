using NUnit.Framework;
using Recipes.Model;
using Recipes.Services;
using System.IO;

namespace UnitTest
{
    [TestFixture]
    internal class RecipeTests
    {
        private string DBFile;
        private RecipeDB DB = new();

        [SetUp]
        public void Init()
        {
            DBFile = Path.GetTempFileName();
            DB.OpenConnection(DBFile);
            DB.CreateTable();
        }

        [Test]
        public void CanCreateRecipe()
        {
            DB.InsertIntoRecipe("Test Rezept");

            var recipes = DB.GetRecipes();
            Assert.That(recipes.Count, Is.EqualTo(1));
            Assert.That(recipes[0].ConntentText, Is.EqualTo("Test Rezept"));
        }

        [TearDown]
        public void Cleanup()
        {
            DB.CloseConnection();
        }
    }
}
