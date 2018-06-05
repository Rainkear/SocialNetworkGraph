using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace SocialNetworkGraph.Utilities
{
    public class ExceptionLogger : IDisposable
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
                return string.Format("{0:HH:mm:ss}: {1}", Date, Message);
            }
        }

        private static readonly ExceptionLogger _instance = new ExceptionLogger();
        private static BlockingCollection<ExceptionInfo> _exceptions = new BlockingCollection<ExceptionInfo>();
        private readonly Task _writerTask = Task.Run(()=>Writer());

        public void LogFile(string message)
        {
            _exceptions.Add(new ExceptionInfo(message, DateTime.Now));
        }

        private static void Writer()
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

        static ExceptionLogger()
        {

        }

        private ExceptionLogger()
        {

        }

        public static ExceptionLogger Instance
        {
            get
            {
                return _instance;
            }
        }

        
    }
}
