using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public class LogSettings
    {
        public LogLevel DefaultLogLevel { get; set; }
        public LogAppenders DefaultAppender { get; set; }
        public List<string> Appenders { get; set; }
    }
}
