using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.Models;

namespace TwitterChannelsExplorer.Core.ServicesInterfaces
{
	public interface ITwitterChannelsService
	{
		int AddChannel(string channelName);
		ChannelsModel GetChannelsModel();
		bool DeleteChannel(int id);
	}
}
