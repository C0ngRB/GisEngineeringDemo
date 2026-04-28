using System;
using System.Collections.Generic;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    public class QueryFeaturesByAttributeUseCase
    {
        private readonly IFeatureQueryPort queryPort;
        private readonly ISelectionPort selectionPort;
        private readonly ILoggerPort logger;

        public QueryFeaturesByAttributeUseCase(
            IFeatureQueryPort queryPort,
            ISelectionPort selectionPort,
            ILoggerPort logger)
        {
            if (queryPort == null) throw new ArgumentNullException("queryPort");
            if (selectionPort == null) throw new ArgumentNullException("selectionPort");
            if (logger == null) throw new ArgumentNullException("logger");

            this.queryPort = queryPort;
            this.selectionPort = selectionPort;
            this.logger = logger;
        }

        public QueryResult Execute(QueryCondition condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (string.IsNullOrWhiteSpace(condition.LayerName))
            {
                throw new ArgumentException("Query layer name is required.", "condition");
            }

            if (string.IsNullOrWhiteSpace(condition.RawWhereClause) &&
                (string.IsNullOrWhiteSpace(condition.FieldName) || string.IsNullOrWhiteSpace(condition.Value)))
            {
                throw new ArgumentException("Attribute query condition is required.", "condition");
            }

            logger.Info("Attribute query: " + condition.LayerName);

            QueryResult result = queryPort.QueryByAttribute(condition);
            selectionPort.SelectFeatures(condition.LayerName, GetObjectIds(result));

            if (result != null)
            {
                logger.Info("Attribute query result count: " + result.TotalCount);
            }

            return result;
        }

        private static IList<int> GetObjectIds(QueryResult result)
        {
            IList<int> objectIds = new List<int>();
            if (result == null || result.Records == null)
            {
                return objectIds;
            }

            for (int i = 0; i < result.Records.Count; i++)
            {
                objectIds.Add(result.Records[i].ObjectId);
            }

            return objectIds;
        }
    }
}
