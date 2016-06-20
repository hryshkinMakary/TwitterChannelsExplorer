using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.EntityFramework.Models;


namespace TwitterChannelsExplorer.EntityFramework.DbRepositories.RepositoryInterfaces
{
	public interface ITwitterCategoriesRepository
	{
		CategoriesModel GetCategoriesModel();
		int SaveCategory(string categoryName);
		bool DeleteCategory(int id);
		bool isCategoryExist(string categoryName);
	}
}
