using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.EntityFramework.Models;

namespace TwitterChannelsExplorer.Core.TwitterServices.ServicesInterfaces
{
	public interface ITwitterChannelsService
	{
		int AddChannel(string channelName);
		ChannelsModel GetChannelsModel();
		bool DeleteChannel(int id);
	}
}
