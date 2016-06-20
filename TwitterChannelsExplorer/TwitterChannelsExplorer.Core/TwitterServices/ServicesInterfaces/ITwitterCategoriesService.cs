using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.EntityFramework.Models;

namespace TwitterChannelsExplorer.Core.TwitterServices.ServicesInterfaces
{
	public interface ITwitterCategoriesService
	{
		CategoriesModel GetCategoriesModel();
		int AddCategory(string categoryName);
		bool DeleteCategory(int id);
	}
}
