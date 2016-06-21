using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.RepositoryInterfaces;
using TwitterChannelsExplorer.EntityFramework.DbRepositories;


namespace TwitterChannelsExplorer.TwitterService.Factories
{
    public class RepositoryFactory
    {
        public static ITwitterChannelsRepository GetChannelsRepository()
        {
			return new ChannelsRepository();
        }
		public static ITwitterCategoriesRepository GetCategoriesRepository()
		{
			return new CategoriesRepository();
		}

		public static ITwitterTweetsRepository GetTweetsRepository()
		{
			return new TweetsRepository();
		}
	}
}
