using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using TwitterChannelsExplorer.ADO.DbRepositories.RepositoryInterfaces;
using TwitterChannelsExplorer.ADO.Models;


namespace TwitterChannelsExplorer.ADO.DbRepositories
{
	public class CategoriesRepository : ITwitterCategoriesRepository
	{
		#region Fields and constants
		private const string TimeFormat = "MM-dd-yyyy hh:mm:ss tt";
		private readonly string _connectionString;
		#endregion

		#region Public methods
		public CategoriesRepository()
		{
			_connectionString = ConfigurationManager.ConnectionStrings["TwitterDatabase"].ConnectionString;
		}
		public CategoriesModel GetCategoriesModel()
		{
			var categoriesModel = new CategoriesModel();

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand command = new SqlCommand("SELECT categoryId,name FROM Categories ORDER By date DESC", connection);
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						categoriesModel.Categories.Add(
							new Category
							{
								Id = reader.GetInt32(0),
								Name = reader.GetString(1)
							});
					}
				}
			}
			return categoriesModel;
		}
		public int SaveCategory(string categoryName)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand command = new SqlCommand("Insert into Categories (name,date) values (@name,@date)", connection);
				command.Parameters.AddWithValue("@name", categoryName);
				command.Parameters.AddWithValue("@date", DateTime.Now.ToString(TimeFormat));
				connection.Open();
				command.ExecuteNonQuery();
				command = new SqlCommand("SELECT @@IDENTITY", connection);
				var result = command.ExecuteScalar();
				return Convert.ToInt32(result);
			}
		}


		public bool isCategoryExist(string categoryName)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand command = new SqlCommand(String.Format("SELECT name FROM Categories WHERE name='{0}'", categoryName), connection);
				connection.Open();
				using (var reader = command.ExecuteReader())
				{
					return reader.HasRows;
				}
			}
		}
		public bool DeleteCategory(int id)
		{
			int uncategorizedId;
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					SqlCommand command = new SqlCommand("SELECT categoryId FROM Categories Where name='uncategorized'", connection);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						reader.Read();
						uncategorizedId = reader.GetInt32(0);
					}
					command = new SqlCommand(String.Format("DELETE FROM Categories WHERE categoryId='{0}'", id), connection);
					command.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		#endregion
	}
}
