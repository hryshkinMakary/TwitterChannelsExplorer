using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterChannelsExplorer.EntityFramework.Exceptions
{
    public class TwitterApplicationExceptions : ApplicationException
    {
		#region Fields and constants
		private static Dictionary<TwitterServiceExceptions, string> exceptions;
		#endregion

		static TwitterApplicationExceptions()
        {
            exceptions = new Dictionary<TwitterServiceExceptions,string>();
            exceptions.Add(TwitterServiceExceptions.LoadTweets,"Failed internet connection");
            exceptions.Add(TwitterServiceExceptions.ChannelExistError,"Twitter channel doesn't exist");
			exceptions.Add(TwitterServiceExceptions.FailedConnectionToDb, "Error connecting to database. Check the connection.");
		}

        public TwitterApplicationExceptions(TwitterServiceExceptions exception)
            :base(exceptions[exception])
        {

        }
    }
}
