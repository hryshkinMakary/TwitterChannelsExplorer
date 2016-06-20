using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.EntityFramework.DbRepositories;
using TwitterChannelsExplorer.EntityFramework.DbRepositories.RepositoryInterfaces;


namespace TwitterChannelsExplorer.Core.Factories
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
