using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Data.SqlClient;
using System.Configuration;
using TwitterChannelsExplorer.EntityFramework.DbRepositories.RepositoryInterfaces;
using TwitterChannelsExplorer.EntityFramework.Models;
using model = TwitterChannelsExplorer.EntityFramework.TwitterEntityModel;

namespace TwitterChannelsExplorer.EntityFramework.DbRepositories
{
	public class TweetsRepository : ITwitterTweetsRepository
	{
		#region Fields and constants
		private const string TimeFormat = "MM-dd-yyyy hh:mm:ss tt";
		private readonly string _connectionString;
		private model.TwitterEntityModel _entityModel;
		#endregion

		#region Public Methods
		public TweetsRepository()
		{
			_entityModel = new model.TwitterEntityModel();
			_connectionString = ConfigurationManager.ConnectionStrings["TwitterDatabase"].ConnectionString;
		}
		public TweetsModel GetTweetsModel(int channelId)
		{
			var tweetsModel = new TweetsModel();
			try
			{
				var channelInfo = _entityModel.Channels.FirstOrDefault(channel => channel.channelId == channelId);
				tweetsModel.Name = channelInfo.name;
				tweetsModel.Src_Name = channelInfo.src_Name;
				tweetsModel.Id = channelInfo.channelId;
				var tweets = from tweet in _entityModel.Tweets
							 where tweet.channelId == channelId
							 orderby tweet.date_time descending
							 select new Tweet
							 {
								 Text = tweet.text,
								 DateTime = tweet.date_time,
								 Src_Image = tweet.src_image,
								 Image = tweet.image,
							 };
				tweetsModel.Tweets.AddRange(tweets);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to retrieve tweets from database", ex);
			}
			return tweetsModel;
		}
		public TweetsModel GetPartialTweetsModel(int channelId, IEnumerable<TwitterStatus> tweets)
		{
			var tweetsModel = new TweetsModel();
			try
			{
				var maxDate = _entityModel.Tweets.Max(tweet => tweet.date_time);
				var newTweets = from tweet in tweets
								orderby tweet.CreatedDate descending
								where tweet.CreatedDate > maxDate
								select tweet;
				SaveTweets(channelId, newTweets);
				tweetsModel.Id = channelId;
				tweetsModel.Tweets.AddRange(
				newTweets.Select(tweet => new Tweet
				{
					Text = tweet.Text,
					DateTime = tweet.CreatedDate,
					Src_Image = tweet.User.ProfileImageUrl,
					Image = (tweet.Entities.Media.Count != 0) ?
					   tweet.Entities.Media.First().MediaUrl : "false"
				}));
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
			try
			{
				_entityModel.Tweets.AddRange(
					tweets.Select(tweet => new model.Tweet
					{
						channelId = id,
						text = tweet.Text,
						date_time = tweet.CreatedDate,
						src_image = tweet.User.ProfileImageUrl,
						image = (tweet.Entities.Media.Count != 0) ?
						   tweet.Entities.Media.First().MediaUrl : "false"
					}));

				_entityModel.SaveChanges();
			}
			catch (Exception ex)
			{
				
			}
		}
		public bool DeleteTweets(int id)
		{
			var tweets = _entityModel.Tweets.Where(p => p.channelId == id);
			_entityModel.Tweets.RemoveRange(tweets);
			_entityModel.SaveChanges();
			return true;
		}
		#endregion
		#region Private Methods

		#endregion
	}
}
