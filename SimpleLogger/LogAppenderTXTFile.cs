using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public class LogAppenderTXTFile : LogAppenderBase
    {
        StreamWriter streamWriter = null;

        public LogAppenderTXTFile(string logQualifiedFileName)
        {
            // Create the Log output Text File
            try
            {
                streamWriter = new StreamWriter(File.Create(logQualifiedFileName));
            }
            catch (UnauthorizedAccessException)
            {
                // The caller does not have the required permission or path specified a file that is read - only.
                throw;
            }
            catch (ArgumentException)
            {
                // path is a zero - length string, contains only white space, or contains one or more invalid characters as defined by InvalidPathChars.
                throw;
            }
            catch (PathTooLongException)
            {
                // The specified path, file name, or both exceed the system - defined maximum length.
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                // The specified path is invalid (for example, it is on an unmapped drive).
                throw;
            }
            catch (IOException)
            {
                // An I/O error occurred while creating the file.
                throw;
            }
            catch (NotSupportedException)
            {
                // path is in an invalid format.
                throw;
            }

            Console.WriteLine("Log file {0} created successfully", logQualifiedFileName);
        }

        // Write asynchroniously
        //public override void FlushToLogger(LogMsg logMsg)
        //{
        //    streamWriter.WriteLine(logMsg.Message);
        //    streamWriter.Flush();
        //}

        // Write asynchroniously
        public override async void FlushToLogger(LogMsg logMsg)
        {
            await streamWriter.WriteLineAsync(logMsg.Message);
            streamWriter.Flush();
        }
    }
}
