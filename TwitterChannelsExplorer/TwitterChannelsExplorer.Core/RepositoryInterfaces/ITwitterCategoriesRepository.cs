using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.Models;


namespace TwitterChannelsExplorer.Core.RepositoryInterfaces
{
	public interface ITwitterCategoriesRepository
	{
		CategoriesModel GetCategoriesModel();
		int SaveCategory(string categoryName);
		bool DeleteCategory(int id);
		bool isCategoryExist(string categoryName);
		IList<string> GetCategoriesAutoCompeteModel(string term);
	}
}
