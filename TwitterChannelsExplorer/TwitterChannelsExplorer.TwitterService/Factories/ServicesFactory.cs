using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.ServicesInterfaces;
using TwitterChannelsExplorer.TwitterService.TwitterServices;

namespace TwitterChannelsExplorer.TwitterService.Factories
{
	public class ServicesFactory
	{
		public static ITwitterChannelsService GetChannelsService()
		{
			return new ChannelsService();
		}
		public static ITwitterCategoriesService GetCategoriesService()
		{
			return new CategoriesService();
		}

		public static ITwitterTweetsService GetTweetsService()
		{
			return new TweetsService();
		}
	}
}
