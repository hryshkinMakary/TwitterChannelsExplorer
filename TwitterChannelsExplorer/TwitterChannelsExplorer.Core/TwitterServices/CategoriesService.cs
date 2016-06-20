using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterChannelsExplorer.Core.TwitterServices.ServicesInterfaces;
using TwitterChannelsExplorer.EntityFramework.DbRepositories.RepositoryInterfaces;
using TwitterChannelsExplorer.EntityFramework.Models;
using TwitterChannelsExplorer.Core.Factories;


namespace TwitterChannelsExplorer.Core.TwitterServices
{
	internal class CategoriesService : ITwitterCategoriesService
	{
		#region Fields and constants
		private ITwitterCategoriesRepository _categoriesRepository;
		private ITwitterChannelsRepository _channelsRepository;
		#endregion

		#region Public methods
		public CategoriesService()
		{
			_categoriesRepository = RepositoryFactory.GetCategoriesRepository();
			_channelsRepository = RepositoryFactory.GetChannelsRepository();
		}
		public CategoriesModel GetCategoriesModel()
		{
			var model = _categoriesRepository.GetCategoriesModel();
			return model;
		}
		public int AddCategory(string categoryName)
		{
			try
			{
				if (_categoriesRepository.isCategoryExist(categoryName))
				{
					return -1;
				}
				else
				{
					int id = _categoriesRepository.SaveCategory(categoryName);
					return id;
				}
			}
			catch (Exception exception)
			{
				return -1;
			}
		}
		public bool DeleteCategory(int id)
		{ 
			if (_categoriesRepository.DeleteCategory(id))
			{
				_channelsRepository.ChangeChannelCategory(id);
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
}
