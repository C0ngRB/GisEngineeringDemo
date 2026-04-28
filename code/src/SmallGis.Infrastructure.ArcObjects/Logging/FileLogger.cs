using System;
using System.Globalization;
using System.IO;
using SmallGis.Application.Ports;

namespace SmallGis.Infrastructure.ArcObjects.Logging
{
    public class FileLogger : ILoggerPort
    {
        private readonly string logDirectory;

        public FileLogger(string logDirectory)
        {
            if (string.IsNullOrWhiteSpace(logDirectory))
            {
                throw new ArgumentException("Log directory is required.", "logDirectory");
            }

            this.logDirectory = logDirectory;
        }

        public void Info(string message)
        {
            Write("INFO", message, null);
        }

        public void Warn(string message)
        {
            Write("WARN", message, null);
        }

        public void Error(string message)
        {
            Write("ERROR", message, null);
        }

        public void Error(string message, Exception ex)
        {
            Write("ERROR", message, ex);
        }

        private void Write(string level, string message, Exception exception)
        {
            Directory.CreateDirectory(logDirectory);
            string fileName = "smallgis_" + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + ".log";
            string path = Path.Combine(logDirectory, fileName);
            string line = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) +
                          " [" + level + "] " +
                          (message ?? string.Empty);

            if (exception != null)
            {
                line = line + " " + exception;
            }

            File.AppendAllText(path, line + Environment.NewLine);
        }
    }
}
