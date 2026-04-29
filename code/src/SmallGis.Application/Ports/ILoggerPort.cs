using System;

namespace SmallGis.Application.Ports
{
    /// <summary>
    /// Minimal logging abstraction used by use cases and infrastructure. / 用例和基础设施共用的最小日志抽象。
    /// </summary>
    public interface ILoggerPort
    {
        void Info(string message);

        void Warn(string message);

        void Error(string message);

        void Error(string message, Exception ex);
    }
}
