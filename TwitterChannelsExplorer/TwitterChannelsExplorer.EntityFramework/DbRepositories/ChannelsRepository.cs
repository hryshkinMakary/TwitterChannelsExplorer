using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using TwitterChannelsExplorer.Core.RepositoryInterfaces;
using TwitterChannelsExplorer.Core.Models;
using model = TwitterChannelsExplorer.EntityFramework.TwitterEntityModel;
using TwitterChannelsExplorer.EntityFramework.Exceptions;

namespace TwitterChannelsExplorer.EntityFramework.DbRepositories
{
	public class ChannelsRepository : ITwitterChannelsRepository
	{
		#region Fields and constants
		private const string TimeFormat = "MM-dd-yyyy hh:mm:ss tt";
		private const int DefaultUncategorizedId = 0;
		private readonly string _connectionString;
		private model.TwitterEntityModel _entityModel;
		#endregion
		public ChannelsRepository()
		{
			_entityModel = new model.TwitterEntityModel();
			_connectionString = ConfigurationManager.ConnectionStrings["TwitterDatabase"].ConnectionString;
		}
		#region Public methods
		public int SaveChannel(string channelName, string source_Name)
		{
			try
			{
				var channel = new model.Channel
				{
					categoryId = 0,
					src_Name = source_Name,
					name = channelName,
					date = DateTime.Now,
				};
				_entityModel.Channels.Add(channel);
				_entityModel.SaveChanges();
				var result = channel.channelId;
				return result;
			}
			catch (Exception exception)
			{
				throw new DBApplicationExceptions(DBServiceExceptions.FailedConnectionToDb);
			}
		}
		public bool DeleteChannel(int id)
		{
			try
			{
				var channel = _entityModel.Channels.Where(p => p.channelId == id).FirstOrDefault();
				_entityModel.Channels.Remove(channel);
				_entityModel.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public ChannelsModel GetChannelsModel()
		{
			try
			{
				var channelsModel = new ChannelsModel();
				var channels = from channel
							   in _entityModel.Channels
							   orderby channel.date descending
							   select new Channel { Id = channel.channelId, Name = channel.src_Name };
				channelsModel.Channels.AddRange(channels);
				return channelsModel;
			}
			catch (Exception exception)
			{
				return null;
			}
		}
		public bool ChangeChannelCategory(int id)
		{
			try
			{
				var channelChange = _entityModel.Channels
									.Where(c => c.categoryId == id)
									.FirstOrDefault();
				channelChange.categoryId = id;
				_entityModel.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		#endregion
	}
}
