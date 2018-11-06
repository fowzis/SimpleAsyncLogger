using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleLogger
{
    /// <summary>
    /// Base class for different loggers, sucj as File logger, Database Logger, EventLog Logger etc... 
    /// The class is abstract to facilitate code reusability by providing virtual function 
    /// and abstract functions to enforce implementation by the derriving class
    /// </summary>
    public sealed class Logger
    {
        // Implement thread-safe singleton pattern
        public static readonly Logger instance = new Logger();

        // Instancite a concurrent collection to que the logging messages
        // Concurrent collection id thread-safe and handles locking and unlocking critical sections during insertion and removal of items.
        private static readonly BlockingCollection<LogMsg> bcLogQue = new BlockingCollection<LogMsg>();

        private List<LogAppenderBase> logAppendersList = null;

        private readonly Task logFlushWorker;
        
        // Define the cancellation token.
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken cancelToken;



        /// <summary>
        /// Well, static constructors in C# are specified to execute only when an instance of the class is created 
        /// or a static member is referenced, and to execute only once per AppDomain. 
        /// </summary>
        static Logger() { }

        private Logger()
        {
            // ToDo - Read Logger Settings from XML File

            // Instanciate the different appenders
            InstanciateAppenders();

            // Start the queue consumer worker task
            cancelToken = cancelTokenSource.Token;
            logFlushWorker = Task.Run(() => LogQueueConsumer(cancelToken), cancelToken);
        }

        ~Logger()
        {
            // Mark the blocking queue as not accepting any more additions
            // Free the writing thread.
            CloseLog();
            cancelTokenSource.Dispose();
        }

        private void InstanciateAppenders()
        {
            logAppendersList = new List<LogAppenderBase>
            {
                new LogAppenderTXTFile(@"d:\temp\testLogFile.txt")
            };
        }

        private void LogQueueConsumer(CancellationToken cancelToken)
        {
            // Remove an item from the queue
            // while (bcLogQue.TryTake(out LogMsg logMsg))
            while (!cancelToken.IsCancellationRequested || bcLogQue.Count > 0)
            {
                // If Cancel is not requested yet and queue is empty, sleep for 1 sec
                if (bcLogQue.Count == 0)
                    Thread.Sleep(1000);
                else
                {
                    foreach (LogMsg logMsg in bcLogQue.GetConsumingEnumerable())
                    {
                        // ToDo - Asynchroniously invoke the FlushToLog on all instanciated Appenders
                        // Take from the queue and send to appenders async
                        foreach (LogAppenderBase logAppender in logAppendersList)
                        {
                            logAppender.FlushToLogger(logMsg);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel">Mandatory - Message Criticality</param>
        /// <param name="strMsg">Mandatory - Human Readable Message to add to the log</param>
        /// <param name="action">Optional - Refers to the calling method</param>
        /// <param name="obj">Optional - Refers to the calling object</param>
        public void Log(LogLevel logLevel, string strMsg, string action="", string obj="")
        {
            // Current class name:
            // this.GetType().Name;

            // Current method name:
            // using System.Reflection;
            // MethodBase.GetCurrentMethod().Name;

            // IF log is off, return and do nothing
            if (logLevel == LogLevel.OFF)
            {
                return;
            }

            LogMsg logMsg = new LogMsg(logLevel, strMsg, action, obj);

            if (String.IsNullOrWhiteSpace(strMsg))
            {
                logMsg.Message = String.Format("Logging Error: Non Human Readable message provided in {0}.{1}",obj, action);
                Console.WriteLine(logMsg.Message);
            }

            // Insert to Queue
            bcLogQue.Add(logMsg);

            //foreach (LogAppenderBase logAppender in logAppendersList)
            //{
            //    logAppender.FlushToLogger(logMsg);
            //}
        }

        public void CloseLog()
        {
            bcLogQue.CompleteAdding();
            cancelTokenSource.Cancel();
            // Wait for the task to complete
            logFlushWorker.Wait();
        }
    }
}
