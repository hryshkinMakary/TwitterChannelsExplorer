using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.Models;

namespace TwitterChannelsExplorer.Core.ServicesInterfaces
{
	public interface ITwitterCategoriesService
	{
		CategoriesModel GetCategoriesModel();
		int AddCategory(string categoryName);
		bool DeleteCategory(int id);
		IList<string> GetCategoriesAutoCompeteModel(string term);
	}
}
