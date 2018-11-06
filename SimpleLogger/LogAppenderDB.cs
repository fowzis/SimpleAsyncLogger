using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    class LogAppenderDB : LogAppenderBase
    {
        public LogAppenderDB()
        {
            GetConnectionString();

            ConnectToDB();
        }

        private void ConnectToDB()
        {
            // Connect to DB using realted DB Connector. ODBC, etc...
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieve connection string from settings
        /// </summary>
        private void GetConnectionString()
        {
            Console.WriteLine("Retrieved Connection String");
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Here we implement the caching and the insertion to DB
        /// </summary>
        /// <param name="logMsg"></param>
        public override void FlushToLogger(LogMsg logMsg)
        {
            Console.WriteLine("Writing Log Msg to DB: {0}", logMsg.ToString());
            //throw new NotImplementedException();
        }
    }
}
