using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public abstract class LogAppenderBase
    {
        /// <summary>
        /// To be implemented by derriving classes
        /// </summary>
        /// <param name="logMsg"></param>
        public abstract void FlushToLogger(LogMsg logMsg);
    }
}
