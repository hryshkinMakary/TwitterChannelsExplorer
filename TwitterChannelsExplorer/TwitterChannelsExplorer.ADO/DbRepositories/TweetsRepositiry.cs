using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Data.SqlClient;
using System.Configuration;
using TwitterChannelsExplorer.Core.RepositoryInterfaces;
using TwitterChannelsExplorer.Core.Models;

namespace TwitterChannelsExplorer.ADO.DbRepositories
{
	public class TweetsRepository : ITwitterTweetsRepository
	{
		#region Fields and constants
		private const string TimeFormat = "MM-dd-yyyy hh:mm:ss tt";
		private readonly string _connectionString;
		#endregion

		#region Public Methods
		public TweetsRepository()
		{
			_connectionString = ConfigurationManager.ConnectionStrings["TwitterDatabase"].ConnectionString;
		}
		public TweetsModel GetTweetsModel(int channelId)
		{
			var tweetsModel = new TweetsModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					SqlCommand command = new SqlCommand(String.Format("SELECT * FROM Tweets,Channels Where Tweets.id='{0}' AND Channels.id ='{0}' ORDER BY date_time DESC", channelId), connection);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						reader.Read();
						tweetsModel.Name = reader.GetString(7);
						tweetsModel.Src_Name = reader.GetString(6);
						tweetsModel.Id = reader.GetInt32(5);
						do
						{
							tweetsModel.Tweets.Add(
								new Tweet
								{
									Text = reader.GetString(1),
									DateTime = reader.GetDateTime(3),
									Src_Image = reader.GetString(2),
									Image = reader.GetString(4)
								});
						}
						while (reader.Read());
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to retrieve tweets from database", ex);
			}
			return tweetsModel;
		}
		public TweetsModel GetPartialTweetsModel(int channelId, IEnumerable<TwitterStatus> tweets)
		{
			DateTime maxDate;
			var tweetsModel = new TweetsModel();
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					SqlCommand command = new SqlCommand("SELECT MAX(date_time) FROM Tweets", connection);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						reader.Read();
						maxDate = reader.GetDateTime(0);
					}
				}
				var newTweets = from tweet
								in tweets
								where tweet.CreatedDate > maxDate
								select tweet;
				SaveTweets(channelId, newTweets);
				foreach (var tweet in newTweets)
				{
					tweetsModel.Id = channelId;
					tweetsModel.Tweets.Add(
						   new Tweet
						   {
							   Text = tweet.Text,
							   DateTime = tweet.CreatedDate,
							   Src_Image = tweet.User.ProfileImageUrl,
							   Image = (tweet.Entities.Media.Count != 0) ?
							   tweet.Entities.Media.First().MediaUrl : "false"
						   });
				}
			}
			catch (Exception ex)
			{
				return null;
				throw new Exception("Failed to retrieve tweets from database", ex);
			}
			return tweetsModel;
		}
		public void SaveTweets(int id, IEnumerable<TwitterStatus> tweets)
		{
			SqlCommand command;
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				foreach (var item in tweets)
				{
					command = new SqlCommand("Insert into Tweets (id,text,src_image,image,date_time) values (@id,@text,@src_image,@image,@date_time)", connection);
					command.Parameters.AddWithValue("id", id);
					command.Parameters.AddWithValue("text", item.Text);
					command.Parameters.AddWithValue("src_image", item.User.ProfileImageUrl);
					if (item.Entities.Media.Count != 0)
					{
						command.Parameters.AddWithValue("image", item.Entities.Media.First().MediaUrl);
					}
					else
					{
						command.Parameters.AddWithValue("image", "false");
					}
					command.Parameters.AddWithValue("date_time", item.CreatedDate.ToString(TimeFormat));
					command.ExecuteNonQuery();
				}
			}
		}
		public bool DeleteTweets(int id)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					SqlCommand command = new SqlCommand(String.Format("DELETE FROM Tweets WHERE id={0}", id), connection);
					connection.Open();
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
