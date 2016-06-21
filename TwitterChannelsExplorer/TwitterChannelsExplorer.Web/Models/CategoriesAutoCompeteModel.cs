using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwitterChannelsExplorer.Core.RepositoryInterfaces;
using TwitterChannelsExplorer.TwitterService.Factories;

namespace TwitterChannelsExplorer.Web.Models
{
	public class CategoriesAutoCompeteModel
	{
		public IList<string> Search(string term)
		{
			var service = RepositoryFactory.GetCategoriesRepository();
			var model = service.GetCategoriesModel();

			return model.Categories.Where(p => p.Name.StartsWith(term, StringComparison.OrdinalIgnoreCase))
				.Select(x => x.Name).ToList();
		}
	}
}