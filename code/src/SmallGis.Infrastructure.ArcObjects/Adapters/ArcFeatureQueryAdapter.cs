using System;
using System.Collections.Generic;
using System.Globalization;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using SmallGis.Application.Ports;
using SmallGis.Domain.Enums;
using SmallGis.Domain.Models;
using SmallGis.Infrastructure.ArcObjects.Mappers;
using SmallGis.Infrastructure.ArcObjects.Utilities;
using DomainQueryResult = SmallGis.Domain.Models.QueryResult;

namespace SmallGis.Infrastructure.ArcObjects.Adapters
{
    public class ArcFeatureQueryAdapter : IFeatureQueryPort
    {
        private readonly IMapControl3 mapControl;
        private readonly ArcFeatureMapper featureMapper;

        public ArcFeatureQueryAdapter(IMapControl3 mapControl)
            : this(mapControl, new ArcFeatureMapper())
        {
        }

        public ArcFeatureQueryAdapter(IMapControl3 mapControl, ArcFeatureMapper featureMapper)
        {
            if (mapControl == null) throw new ArgumentNullException("mapControl");

            this.mapControl = mapControl;
            this.featureMapper = featureMapper;
        }

        public DomainQueryResult QueryByAttribute(QueryCondition condition)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            IQueryFilter filter = new QueryFilterClass();
            filter.WhereClause = BuildWhereClause(condition);
            return ExecuteSearch(condition.LayerName, filter, 0);
        }

        public IList<FeatureRecord> GetAllFeatures(string layerName, int maxCount)
        {
            DomainQueryResult result = ExecuteSearch(layerName, null, maxCount);
            return result.Records;
        }

        private DomainQueryResult ExecuteSearch(string layerName, IQueryFilter filter, int maxCount)
        {
            IFeatureCursor cursor = null;
            try
            {
                IFeatureLayer layer = FindFeatureLayer(layerName);
                if (layer == null || layer.FeatureClass == null)
                {
                    throw new ArgumentException("Feature layer not found.", "layerName");
                }

                DomainQueryResult result = new DomainQueryResult
                {
                    LayerName = layer.Name
                };

                cursor = layer.FeatureClass.Search(filter, true);
                IFeature feature = cursor.NextFeature();
                while (feature != null)
                {
                    FeatureRecord record = featureMapper.Map(feature, layer.Name);
                    if (record != null)
                    {
                        result.Records.Add(record);
                    }

                    if (maxCount > 0 && result.Records.Count >= maxCount)
                    {
                        break;
                    }

                    feature = cursor.NextFeature();
                }

                result.TotalCount = result.Records.Count;
                result.Message = result.TotalCount == 0 ? "No matching features found." : string.Empty;
                return result;
            }
            catch (Exception ex)
            {
                throw ArcObjectsExceptionMapper.ToApplicationException("Feature query failed.", ex);
            }
            finally
            {
                ArcObjectsComReleaser.Release(cursor);
            }
        }

        private IFeatureLayer FindFeatureLayer(string layerName)
        {
            IMap map = mapControl.Map;
            if (map == null)
            {
                return null;
            }

            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer != null && string.Equals(layer.Name, layerName, StringComparison.OrdinalIgnoreCase))
                {
                    return layer as IFeatureLayer;
                }
            }

            return null;
        }

        private static string BuildWhereClause(QueryCondition condition)
        {
            if (!string.IsNullOrWhiteSpace(condition.RawWhereClause))
            {
                return condition.RawWhereClause;
            }

            string value = FormatValue(condition.Value);
            string op = MapOperator(condition.Operator);
            if (condition.Operator == QueryOperator.Like)
            {
                value = "'%" + EscapeSqlText(condition.Value) + "%'";
            }

            return condition.FieldName + " " + op + " " + value;
        }

        private static string MapOperator(QueryOperator queryOperator)
        {
            switch (queryOperator)
            {
                case QueryOperator.NotEqual:
                    return "<>";
                case QueryOperator.GreaterThan:
                    return ">";
                case QueryOperator.GreaterOrEqual:
                    return ">=";
                case QueryOperator.LessThan:
                    return "<";
                case QueryOperator.LessOrEqual:
                    return "<=";
                case QueryOperator.Like:
                    return "LIKE";
                default:
                    return "=";
            }
        }

        private static string FormatValue(string value)
        {
            double numericValue;
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out numericValue))
            {
                return numericValue.ToString(CultureInfo.InvariantCulture);
            }

            return "'" + EscapeSqlText(value) + "'";
        }

        private static string EscapeSqlText(string value)
        {
            return (value ?? string.Empty).Replace("'", "''");
        }
    }
}
