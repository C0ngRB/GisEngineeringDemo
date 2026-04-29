using System;
using System.Collections.Generic;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    /// <summary>
    /// Executes an attribute query and synchronizes returned object IDs to selection. / 执行属性查询，并将返回的 ObjectID 同步到地图选择集。
    /// </summary>
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
            // Keep the map selection in sync with the query result while UI only sees domain records. / 保持地图选择集与查询结果同步，同时 UI 只接触领域记录。
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
