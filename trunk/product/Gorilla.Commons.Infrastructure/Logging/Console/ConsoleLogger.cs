using System;
using System.IO;
using System.Threading;
using Gorilla.Commons.Utility.Extensions;

namespace Gorilla.Commons.Infrastructure.Logging.Console
{
    public class ConsoleLogger : ILogger
    {
        readonly TextWriter writer;

        public ConsoleLogger() : this(System.Console.Out)
        {
        }

        public ConsoleLogger(TextWriter writer)
        {
            this.writer = writer;
        }

        public void informational(string formatted_string, params object[] arguments)
        {
            writer.WriteLine(formatted_string, arguments);
        }

        public void debug(string formatted_string, params object[] arguments)
        {
            writer.WriteLine("Thread: {0}, {1}", Thread.CurrentThread.ManagedThreadId,
                             formatted_string.formatted_using(arguments));
        }

        public void error(Exception e)
        {
            writer.WriteLine("{0}", e);
        }
    }
}