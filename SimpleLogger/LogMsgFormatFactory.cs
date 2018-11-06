using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public class LogMsgFormatFactory
    {
        public string GetFormatedMsg(LogLevel logLevel, LogMsg logMsg)
        {
            string formattedMessage = null;

            // Here we can create different message formatting classes.
            switch (logLevel)
            {
                case LogLevel.DEBUG:
                    const string DEBUG_MSG = "[{3}] * Warning: {0} (Action {1} on {2})";
                    formattedMessage = String.Format(DEBUG_MSG, logMsg.Message, logMsg.Action, logMsg.Object, LogTimeStamp());
                    break;
                case LogLevel.INFO:
                    const string INFO_MSG = "[{0}] * Info: {1}";
                    formattedMessage = String.Format(INFO_MSG, LogTimeStamp(), logMsg.Message);
                    break;
                case LogLevel.WARN:
                    const string WARNING_MSG = "[{3}] * Warning: {0} (Action {1} on {2})";
                    formattedMessage = String.Format(WARNING_MSG, logMsg.Message, logMsg.Action, logMsg.Object, LogTimeStamp());
                    break;
                case LogLevel.ERROR:
                    const string ERROR_MSG = "[{3}] * Error: {0} (Action {1} on {2})";
                    formattedMessage = String.Format(ERROR_MSG, logMsg.Message, logMsg.Action, logMsg.Object, LogTimeStamp());
                    break;
                case LogLevel.FATAL:
                    const string FATAL_MSG = "[{3}] * Fatal: {0} (Action {1} on {2})";
                    formattedMessage = String.Format(FATAL_MSG, logMsg.Message, logMsg.Action, logMsg.Object, LogTimeStamp());
                    break;
            }

            return formattedMessage;
        }

        private string LogTimeStamp()
        {
            DateTime now = DateTime.Now;
            return now.ToShortTimeString();
        }
    }
}
