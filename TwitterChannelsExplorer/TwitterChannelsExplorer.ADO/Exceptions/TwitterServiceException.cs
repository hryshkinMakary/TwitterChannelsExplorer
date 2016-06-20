using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterChannelsExplorer.ADO.Exceptions
{
    public enum TwitterServiceExceptions
	{
        LoadTweets,
        ChannelExistError,
		FailedConnectionToDb
    }
}
