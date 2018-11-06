using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public enum LogLevel
    {
        ALL,
        DEBUG,
        INFO,
        WARN,
        ERROR,
        FATAL,
        OFF
    }

    // Define an Enum with FlagsAttribute.
    [FlagsAttribute]
    public enum LogAppenders
    {
        FileTXT = 0,
        FilePDF = 1,
        EventLog = 2,
        DataBase = 4,
        RemoteLogger = 8,
        StdOut = 16
    }

    public class LogMsg
    {
        // Instanciate once per App.Domain on first use
        private static readonly LogMsgFormatFactory MsgFormatter = new LogMsgFormatFactory();

        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public string Object { get; set; }

        public LogMsg() { }

        // Constructor with optional parameters
        public LogMsg(LogLevel logLevel, string strMsg = "", string strAction = "", string strObject = "")
        {
            this.Level = logLevel;
            this.Message = strMsg;
            this.Action = strAction;
            this.Object = strObject;

            // Format and overwrite the original message text
            this.Message = MsgFormatter.GetFormatedMsg(logLevel, this);
        }

        public override string ToString()
        {
            const string DEBUG_MSG = "[{3}] * Warning: {0} (Action {1} on {2})";
            return String.Format(DEBUG_MSG, this.Message, this.Action, this.Object, DateTime.Now.ToShortTimeString());
        }
    }
}
