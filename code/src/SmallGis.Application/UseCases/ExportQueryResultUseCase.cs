using System;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    /// <summary>
    /// Exports a query result through the configured export port. / 通过已配置的导出端口导出查询结果。
    /// </summary>
    public class ExportQueryResultUseCase
    {
        private readonly IQueryResultExportPort exportPort;
        private readonly ILoggerPort logger;

        public ExportQueryResultUseCase(IQueryResultExportPort exportPort, ILoggerPort logger)
        {
            if (exportPort == null) throw new ArgumentNullException("exportPort");
            if (logger == null) throw new ArgumentNullException("logger");

            this.exportPort = exportPort;
            this.logger = logger;
        }

        public void Execute(QueryResult result, string outputPath)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            if (string.IsNullOrWhiteSpace(outputPath))
            {
                throw new ArgumentException("Export path is required.", "outputPath");
            }

            logger.Info("Export query result: " + outputPath);
            exportPort.ExportToCsv(result, outputPath);
        }
    }
}
