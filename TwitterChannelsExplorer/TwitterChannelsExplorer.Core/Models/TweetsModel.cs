using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterChannelsExplorer.Core.Models
{
	public class TweetsModel
	{
		#region Fileds and constants
		public string Name { get; set; }
		public string Src_Name { get; set; }
		public int Id { get; set; }
		public List<Tweet> Tweets { get; set; }
		#endregion
		public TweetsModel()
		{
			Tweets = new List<Tweet>();
		}
	}

	public class Tweet
	{
		#region Fields and constants
		public string Text { get; set; }
		public DateTime DateTime { get; set; }
		public string Image { get; set; }
		public string Src_Image { get; set; }
		#endregion
	}
}
