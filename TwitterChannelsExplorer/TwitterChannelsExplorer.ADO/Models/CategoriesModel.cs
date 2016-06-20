using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterChannelsExplorer.ADO.Models
{
	public class CategoriesModel
	{
		#region Fields and constants
		public IList<Category> Categories { get; set; }
		#endregion
		public CategoriesModel()
		{
			Categories = new List<Category>();
		}
	}

	public class Category
	{
		#region Fileds and Constants
		public string Name { get; set; }
		public int Id { get; set; }
		#endregion
	}
}
