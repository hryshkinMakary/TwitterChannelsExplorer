using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterChannelsExplorer.ADO.Exceptions
{
    public class DBApplicationExceptions : ApplicationException
    {
		#region Fields and constants
		private static Dictionary<DBServiceExceptions, string> exceptions;
		#endregion

		static DBApplicationExceptions()
        {
            exceptions = new Dictionary<DBServiceExceptions,string>();
			exceptions.Add(DBServiceExceptions.FailedConnectionToDb, "Error connecting to database. Check the connection.");
		}

        public DBApplicationExceptions(DBServiceExceptions exception)
            :base(exceptions[exception])
        {

        }
    }
}
