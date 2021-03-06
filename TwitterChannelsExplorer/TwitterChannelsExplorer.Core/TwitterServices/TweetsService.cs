﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterChannelsExplorer.Core.TwitterServices.ServicesInterfaces;
using TwitterChannelsExplorer.EntityFramework.DbRepositories.RepositoryInterfaces;
using TwitterChannelsExplorer.EntityFramework.Models;
using TwitterChannelsExplorer.Core.Factories;
using TwitterChannelsExplorer.Core;

namespace TwitterChannelsExplorer.Core.TwitterServices
{
	internal class TweetsService : ITwitterTweetsService
	{
		#region Fields and constants
		private ITwitterTweetsRepository _tweetsRepository;
		private TwitterApi _twitterApi;
		private const int TweetsCount = 50;
		#endregion

		#region Public methods
		public TweetsService()
		{
			_twitterApi = new TwitterApi();
			_tweetsRepository = RepositoryFactory.GetTweetsRepository();
		}

		public TweetsModel GetTweetsModel(int channelId)
		{
			try
			{
				var model = _tweetsRepository.GetTweetsModel(channelId);
				return model;
			}
			catch(Exception exception)
			{
				throw exception;
			}
		}

		public TweetsModel GetPartialTweetsModel(int channelId, string channelName)
		{
			var tweets = _twitterApi.GetTweets(channelName, TweetsCount);
			var model = _tweetsRepository.GetPartialTweetsModel(channelId, tweets);
			return model;
		}
		#endregion
	}
}
