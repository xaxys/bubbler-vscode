namespace LoggerNs
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading;


    public class LogTextWriter : TextWriter
    {
        public override Encoding Encoding => throw new NotImplementedException();
        private static readonly string home = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
        private static readonly ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        private static bool done = false;
        const string log = ".bubbler.log";
        private bool enable = false;
        public bool Enable { get { return enable; } set { enable = value; CleanUpLogFile(); } }
        public string Prefix { get; set; } = "";
        public string Path => Prefix == "" ? home + System.IO.Path.DirectorySeparatorChar + log : Prefix + System.IO.Path.DirectorySeparatorChar + log;

        public LogTextWriter() {
            CleanUpLogFile();
        }

        ~LogTextWriter()
        {
            CleanUpLogFile();
        }

        // This method must only be called from the client so multiple instances of the
        // server doesn't blow away the file after the client already had.
        public void CleanUpLogFile()
        {
            if (done) return;
            done = true;
            cacheLock.EnterWriteLock();
            try
            {
                if (!enable) return;
                File.Delete(Path);
                using (StreamWriter w = File.AppendText(Path))
                {
                    w.WriteLine("Logging for Bubbler Language Server started " + DateTime.Now.ToString());
                }
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public override void Write(string message)
        {
            cacheLock.EnterWriteLock();
            try
            {
                if (!enable) return;
                using (StreamWriter w = File.AppendText(Path))
                {
                    w.Write(message);
                }
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public override void WriteLine(string message)
        {
            cacheLock.EnterWriteLock();
            try
            {
                if (!enable) return;
                using (StreamWriter w = File.AppendText(Path))
                {
                    w.WriteLine(message);
                }
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public override void WriteLine()
        {
            cacheLock.EnterWriteLock();
            try
            {
                if (!enable) return;
                using (StreamWriter w = File.AppendText(Path))
                {
                    w.WriteLine();
                }
            }
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }

        public void Notify(string message)
        {
            WriteLine(message);
        }
    }

    public class Logger
    {
        public static LogTextWriter Log { get; set; } = new LogTextWriter();
    }
}
