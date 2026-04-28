using System;
using System.Collections.Generic;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    public class QueryFeaturesBySpatialRelationUseCase
    {
        private readonly ISpatialQueryPort spatialQueryPort;
        private readonly ISelectionPort selectionPort;
        private readonly ILoggerPort logger;

        public QueryFeaturesBySpatialRelationUseCase(
            ISpatialQueryPort spatialQueryPort,
            ISelectionPort selectionPort,
            ILoggerPort logger)
        {
            if (spatialQueryPort == null) throw new ArgumentNullException("spatialQueryPort");
            if (selectionPort == null) throw new ArgumentNullException("selectionPort");
            if (logger == null) throw new ArgumentNullException("logger");

            this.spatialQueryPort = spatialQueryPort;
            this.selectionPort = selectionPort;
            this.logger = logger;
        }

        public QueryResult Execute(SpatialQueryCondition condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            if (string.IsNullOrWhiteSpace(condition.TargetLayerName))
            {
                throw new ArgumentException("Target layer name is required.", "condition");
            }

            logger.Info("Spatial query: " + condition.TargetLayerName);

            QueryResult result;
            if (condition.QueryExtent != null)
            {
                result = spatialQueryPort.QueryByExtent(condition);
            }
            else
            {
                result = spatialQueryPort.QueryBySelectedFeature(condition);
            }

            selectionPort.SelectFeatures(condition.TargetLayerName, GetObjectIds(result));

            if (result != null)
            {
                logger.Info("Spatial query result count: " + result.TotalCount);
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
