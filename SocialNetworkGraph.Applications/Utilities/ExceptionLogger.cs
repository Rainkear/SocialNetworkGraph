using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialNetworkGraph.Utilities
{
    public sealed class ExceptionLogger : IDisposable
    {
        private class ExceptionInfo
        {
            public ExceptionInfo(string message, DateTime date)
            {
                Message = message;
                Date = date;
            }

            public string Message { get; set; }
            public DateTime Date { get; set; }

            public override string ToString()
            {
                return string.Format("{0:HH:mm:ss}: {1}\r\n", Date, Message);
            }
        }

        private static readonly Lazy<ExceptionLogger> lazy =
                new Lazy<ExceptionLogger>(() => new ExceptionLogger());
        private BlockingCollection<ExceptionInfo> _exceptions = new BlockingCollection<ExceptionInfo>();
        private readonly Task _writerTask;

        public void LogFile(string message)
        {
            string messageWithoutEmptyLines = Regex.Replace(message, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
            _exceptions.Add(new ExceptionInfo(messageWithoutEmptyLines, DateTime.Now));
        }

        private void Writer()
        {
                string directory = Path.Combine(Directory.GetCurrentDirectory(), "Logs\\");

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                foreach (var ex in _exceptions.GetConsumingEnumerable())
                {
                    string filename = Path.Combine(directory, string.Format("{0:yyyyMMdd}.log", ex.Date));
                    using (Stream stream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        stream.Position = stream.Length;
                        StreamWriter writer = new StreamWriter(stream);
                        writer.WriteLine(ex.ToString());
                        writer.Flush();
                    }
                }           
        }

        public void Dispose()
        {
            _exceptions.CompleteAdding();
        }

        private ExceptionLogger()
        {
            _writerTask = Task.Run(() => Writer());
        }

        public static ExceptionLogger Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        
    }
}
