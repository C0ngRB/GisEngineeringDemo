using System;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
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
