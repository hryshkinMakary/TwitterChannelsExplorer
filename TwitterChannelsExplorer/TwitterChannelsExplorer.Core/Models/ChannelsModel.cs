using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterChannelsExplorer.Core.Models
{
	public class ChannelsModel
	{
		#region Fields and constants
		public List<Channel> Channels { get; set; }
		public ChannelsModel()
		{
			Channels = new List<Channel>();
		}
		#endregion
	}

	public class Channel
	{
		#region Filelds and constants
		public string Name { get; set; }
		public int Id { get; set; }
		#endregion
	}
}
