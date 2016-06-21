using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using TwitterChannelsExplorer.Core.RepositoryInterfaces;
using TwitterChannelsExplorer.Core.Models;
using TwitterChannelsExplorer.ADO.DbRepositories;
using TwitterChannelsExplorer.ADO.Exceptions;

namespace TwitterChannelsExplorer.ADO.DbRepositories
{
	public class ChannelsRepository : ITwitterChannelsRepository
	{
		#region Fields and constants
		private const string TimeFormat = "MM-dd-yyyy hh:mm:ss tt";
		private const int DefaultUncategorizedId = 0;
		private readonly string _connectionString;
		#endregion

		#region Public methods
		public ChannelsRepository()
		{
		_connectionString = ConfigurationManager.ConnectionStrings["TwitterDatabase"].ConnectionString;
		}
		public int SaveChannel(string channelName, string source_Name)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					SqlCommand command = new SqlCommand("Insert into Channels (src_Name,name,date,categoryId) values (@src_Name,@name,@date,@categoryId)", connection);
					command.Parameters.AddWithValue("@src_Name", channelName);
					command.Parameters.AddWithValue("@name", source_Name);
					command.Parameters.AddWithValue("@date", DateTime.Now.ToString(TimeFormat));
					command.Parameters.AddWithValue("@categoryId", DefaultUncategorizedId);
					connection.Open();
					command.ExecuteNonQuery();
					command = new SqlCommand("SELECT @@IDENTITY", connection);
					var result = command.ExecuteScalar();
					return Convert.ToInt32(result);
				}
			}
			catch(Exception exception)
			{
				throw new DBApplicationExceptions(DBServiceExceptions.FailedConnectionToDb);
			}
		}
		public bool DeleteChannel(int id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					SqlCommand command = new SqlCommand(String.Format("DELETE FROM Channels WHERE id={0}", id), connection);
					command.ExecuteNonQuery();
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public ChannelsModel GetChannelsModel()
		{
			var channelsModel = new ChannelsModel();

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				SqlCommand command = new SqlCommand("SELECT id,src_Name FROM Channels ORDER By date DESC", connection);
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						channelsModel.Channels.Add(
							new Channel
							{
								Id = reader.GetInt32(0),
								Name = reader.GetString(1)
							});
					}
				}
			}
			return channelsModel;
		}
		public bool ChangeChannelCategory(int id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					
					SqlCommand command = new SqlCommand(String.Format("UPDATE Channels SET categoryId='{0}' WHERE categoryId='{1}'", 0, id), connection);
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
