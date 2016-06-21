using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using TweetSharp;
using TwitterServiceEntity = TweetSharp.TwitterService;
using TwitterChannelsExplorer.TwitterService.Exceptions;

namespace TwitterChannelsExplorer.TwitterService
{
	internal class TwitterApi
	{
		#region Fields and constants
		private TwitterServiceEntity _service;
		#endregion
		public TwitterApi()
		{
			_service = new TwitterServiceEntity(ConfigurationManager.AppSettings.Get("consumerKey"),
				ConfigurationManager.AppSettings.Get("consumerSecret"));
			_service.AuthenticateWith(ConfigurationManager.AppSettings.Get("token"),
				ConfigurationManager.AppSettings.Get("tokenSecret"));
		}
		#region Public methods
		public IEnumerable<TwitterStatus> GetTweets(string channelName, int tweetsNumber)
		{
			var options = new ListTweetsOnUserTimelineOptions()
			{
				ScreenName = channelName,
				Count = tweetsNumber
			};
			if (options == null)
			{
				throw new TwitterApplicationExceptions(TwitterServiceExceptions.LoadTweets);
			}
			return _service.ListTweetsOnUserTimeline(options);
		}
		#endregion
	}
}
