using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SimpleLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger log = Logger.instance;

            for (int i = 0; i < 10000; i++)
            {
                log.Log(LogLevel.DEBUG, i + " :Msg Fowzi", MethodBase.GetCurrentMethod().Name, "Program");
            }

            log.CloseLog();
            Console.WriteLine("Exiting Program");
        }
    }
}
