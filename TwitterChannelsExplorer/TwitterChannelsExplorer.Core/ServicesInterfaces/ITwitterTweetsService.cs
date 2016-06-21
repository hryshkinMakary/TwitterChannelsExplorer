using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.Models;

namespace TwitterChannelsExplorer.Core.ServicesInterfaces
{
	public interface ITwitterTweetsService
	{
		TweetsModel GetTweetsModel(int channelId);
		TweetsModel GetPartialTweetsModel(int channelId, string channelName);
	}
}
