using System;
using System.Collections.Generic;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    public class ShowAttributeTableUseCase
    {
        private readonly IFeatureQueryPort featureQueryPort;
        private readonly ILoggerPort logger;

        public ShowAttributeTableUseCase(IFeatureQueryPort featureQueryPort, ILoggerPort logger)
        {
            if (featureQueryPort == null) throw new ArgumentNullException("featureQueryPort");
            if (logger == null) throw new ArgumentNullException("logger");

            this.featureQueryPort = featureQueryPort;
            this.logger = logger;
        }

        public QueryResult Execute(string layerName, int maxCount)
        {
            if (string.IsNullOrWhiteSpace(layerName))
            {
                throw new ArgumentException("Layer name is required.", "layerName");
            }

            logger.Info("Show attribute table: " + layerName);
            IList<FeatureRecord> records = featureQueryPort.GetAllFeatures(layerName, maxCount);
            QueryResult result = new QueryResult
            {
                LayerName = layerName,
                TotalCount = records == null ? 0 : records.Count,
                Message = records == null || records.Count == 0 ? "No records found." : string.Empty
            };

            if (records != null)
            {
                for (int i = 0; i < records.Count; i++)
                {
                    result.Records.Add(records[i]);
                }
            }

            return result;
        }
    }
}
