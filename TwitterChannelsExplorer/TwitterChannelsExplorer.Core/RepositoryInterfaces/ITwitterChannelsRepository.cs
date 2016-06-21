using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterChannelsExplorer.Core.Models;

namespace TwitterChannelsExplorer.Core.RepositoryInterfaces
{
	public interface ITwitterChannelsRepository
	{
		int SaveChannel(string channelName, string sourceName);
		bool DeleteChannel(int id);
		ChannelsModel GetChannelsModel();
		bool ChangeChannelCategory(int id);
	}
}
