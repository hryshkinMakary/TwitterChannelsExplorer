using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using TwitterChannelsExplorer.Core.RepositoryInterfaces;
using  Models = TwitterChannelsExplorer.Core.Models;
using TwitterChannelsExplorer.EntityFramework.TwitterEntityModel;
using model = TwitterChannelsExplorer.EntityFramework.TwitterEntityModel;

namespace TwitterChannelsExplorer.EntityFramework.DbRepositories
{
	public class CategoriesRepository : ITwitterCategoriesRepository
	{
		#region Fields and constants
		private const string TimeFormat = "MM-dd-yyyy hh:mm:ss tt";
		private readonly string _connectionString;
		private model.TwitterEntityModel _entityModel;
		#endregion

		#region Public methods
		public CategoriesRepository()
		{
			_entityModel = new model.TwitterEntityModel();
			_connectionString = ConfigurationManager.ConnectionStrings["TwitterDatabase"].ConnectionString;
		}
		public Models.CategoriesModel GetCategoriesModel()
		{
			var categoriesModel = new Models.CategoriesModel();
			var categories = from category
						   in _entityModel.Categories
						   orderby category.date descending
						   select new Models.Category { Id = category.categoryId, Name = category.name };
			categoriesModel.Categories.AddRange(categories);
			return categoriesModel;
		}
		public int SaveCategory(string categoryName)
		{
			var category = new model.Category
			{
				name = categoryName,
				date = DateTime.Now
			};
			_entityModel.Categories.Add(category);
			_entityModel.SaveChanges();
			var result = category.categoryId;
			return result;
		}
		public bool isCategoryExist(string categoryName)
		{
			var result = _entityModel.Categories.FirstOrDefault(name=>name.name==categoryName);
			return (result == null) ? false : true;
		}
		public bool DeleteCategory(int id)
		{
			try
			{
				var category = _entityModel.Categories.Where(p => p.categoryId == id).FirstOrDefault();
				_entityModel.Categories.Remove(category);
				_entityModel.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public IList<string> GetCategoriesAutoCompeteModel(string term)
		{
			return _entityModel.Categories.Where(p => p.name.StartsWith(term))
				.Select(p => p.name).ToList();
		}
		#endregion
	}
}
