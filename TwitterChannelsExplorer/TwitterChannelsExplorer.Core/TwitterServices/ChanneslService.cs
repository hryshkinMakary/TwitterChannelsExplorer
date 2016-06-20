using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.TwitterServices.ServicesInterfaces;
using TwitterChannelsExplorer.EntityFramework.DbRepositories.RepositoryInterfaces;
using TwitterChannelsExplorer.EntityFramework.DbRepositories;
using TwitterChannelsExplorer.EntityFramework.Models;
using TwitterChannelsExplorer.Core.Factories;


namespace TwitterChannelsExplorer.Core.TwitterServices
{
	internal class ChannelsService : ITwitterChannelsService
	{
		#region Fields and constants
		private ITwitterChannelsRepository _channelsRepository;
		private ITwitterTweetsRepository _tweetsRepository;
		private TwitterApi _twitterApi;
		private const int TweetsCount = 50;
		#endregion

		#region Public methods
		public ChannelsService()
		{
			_twitterApi = new TwitterApi();
			_channelsRepository = RepositoryFactory.GetChannelsRepository();
			_tweetsRepository = RepositoryFactory.GetTweetsRepository();
		}
		public int AddChannel(string channelName)
		{
			try
			{
				var tweets = _twitterApi.GetTweets(channelName, TweetsCount);
				int id = _channelsRepository.SaveChannel(tweets.First().User.Name,channelName);
				_tweetsRepository.SaveTweets(id, tweets);
				return id;
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
		public ChannelsModel GetChannelsModel()
		{
			return _channelsRepository.GetChannelsModel();
		}
		public bool DeleteChannel(int id)
		{
			if (_tweetsRepository.DeleteTweets(id) && _channelsRepository.DeleteChannel(id))
			{

				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion

	}
}
