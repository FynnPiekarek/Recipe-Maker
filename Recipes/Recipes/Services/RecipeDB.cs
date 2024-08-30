using Microsoft.Data.Sqlite;
using Recipes.Model;

namespace Recipes.Services
{
    public class RecipeDB
    {
        public SqliteConnection connection = new SqliteConnection();

        public void OpenConnection(string databaseFileName)
        {
            connection = new SqliteConnection("Data Source=" + databaseFileName);
            connection.Open();
        }

        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
        public void CreateTable()
        {

            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Recipe([ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [ConntentText] NVARCHAR(800) NOT NULL)";
                command.ExecuteNonQuery();
            }
        }

        public void InsertIntoRecipe(string ConntentText)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"INSERT INTO Recipe([ConntentText]) VALUES ('{ConntentText}')";
                command.ExecuteNonQuery();
            }

        }

        public List<Recipe> GetRecipes()
        {
            var list = new List<Recipe>();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM Recipe";
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    Recipe recipe = new Recipe();
                    recipe.Id = result.GetInt32(0);
                    recipe.ConntentText = result.GetString(1);
                    list.Add(recipe);
                }

            }
            return list;
        }

        public Recipe GetOneRecipe(int id)
        {

            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM Recipe WHERE ID = {id}";
                var result = command.ExecuteReader();
                while (result.Read())
                {
                    Recipe recipe = new Recipe();

                    recipe.Id = result.GetInt32(0);
                    recipe.ConntentText = result.GetString(1);
                    return recipe;
                }

            }
            return null;
        }


        public void DeleteRecipe(int id)
        {
            using (var command = connection.CreateCommand())
            {
                string query = $"DELETE FROM Recipe WHERE ID = '{id}'";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }

        public void EditRecipe(int id, string changedattribute, string newvalue)
        {
            using (var command = connection.CreateCommand())
            {
                string query = $"UPDATE Recipe SET {changedattribute} = '{newvalue}' WHERE ID = '{id}'";
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }
    }
}
