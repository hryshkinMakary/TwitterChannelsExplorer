using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterChannelsExplorer.ADO.Models;

namespace TwitterChannelsExplorer.ADO.DbRepositories.RepositoryInterfaces
{
	public interface ITwitterTweetsRepository
	{
		void SaveTweets(int id, IEnumerable<TwitterStatus> tweets);
		bool DeleteTweets(int id);
		TweetsModel GetTweetsModel(int channelId);
		TweetsModel GetPartialTweetsModel(int channelId, IEnumerable<TwitterStatus> tweets);
	}
}
