using System;

namespace SmallGis.Application.Ports
{
    public interface ILoggerPort
    {
        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Error(string message, Exception ex);
    }
}
