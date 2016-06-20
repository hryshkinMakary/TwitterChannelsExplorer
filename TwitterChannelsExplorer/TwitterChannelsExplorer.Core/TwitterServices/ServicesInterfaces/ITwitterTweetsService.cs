using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.EntityFramework.Models;

namespace TwitterChannelsExplorer.Core.TwitterServices.ServicesInterfaces
{
	public interface ITwitterTweetsService
	{
		TweetsModel GetTweetsModel(int channelId);
		TweetsModel GetPartialTweetsModel(int channelId, string channelName);
	}
}
