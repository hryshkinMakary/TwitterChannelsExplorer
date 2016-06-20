using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.TwitterServices.ServicesInterfaces;
using TwitterChannelsExplorer.Core.TwitterServices;

namespace TwitterChannelsExplorer.Core.Factories
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
