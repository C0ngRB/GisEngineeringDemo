using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;
using SmallGis.Infrastructure.ArcObjects.Mappers;
using SmallGis.Infrastructure.ArcObjects.Utilities;
using DomainQueryResult = SmallGis.Domain.Models.QueryResult;

namespace SmallGis.Infrastructure.ArcObjects.Adapters
{
    public class ArcSpatialQueryAdapter : ISpatialQueryPort
    {
        private readonly IMapControl3 mapControl;
        private readonly ArcGeometryMapper geometryMapper;
        private readonly ArcFeatureMapper featureMapper;

        public ArcSpatialQueryAdapter(IMapControl3 mapControl)
            : this(mapControl, new ArcGeometryMapper(), new ArcFeatureMapper())
        {
        }

        public ArcSpatialQueryAdapter(IMapControl3 mapControl, ArcGeometryMapper geometryMapper, ArcFeatureMapper featureMapper)
        {
            if (mapControl == null) throw new ArgumentNullException("mapControl");

            this.mapControl = mapControl;
            this.geometryMapper = geometryMapper;
            this.featureMapper = featureMapper;
        }

        public DomainQueryResult QueryByExtent(SpatialQueryCondition condition)
        {
            if (condition == null) throw new ArgumentNullException("condition");
            if (condition.QueryExtent == null) throw new ArgumentException("Query extent is required.", "condition");

            IEnvelope envelope = geometryMapper.ToEnvelope(condition.QueryExtent);
            return ExecuteSpatialQuery(condition, envelope);
        }

        public DomainQueryResult QueryBySelectedFeature(SpatialQueryCondition condition)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            IGeometry geometry = GetSelectedFeatureGeometry(condition.SourceLayerName, condition.SourceFeatureObjectId);
            if (geometry == null)
            {
                throw new ArgumentException("Source selected feature was not found.", "condition");
            }

            return ExecuteSpatialQuery(condition, geometry);
        }

        private DomainQueryResult ExecuteSpatialQuery(SpatialQueryCondition condition, IGeometry queryGeometry)
        {
            IFeatureCursor cursor = null;
            try
            {
                IFeatureLayer layer = FindFeatureLayer(condition.TargetLayerName);
                if (layer == null || layer.FeatureClass == null)
                {
                    throw new ArgumentException("Target feature layer not found.", "condition");
                }

                ISpatialFilter filter = new SpatialFilterClass();
                filter.Geometry = queryGeometry;
                filter.GeometryField = layer.FeatureClass.ShapeFieldName;
                filter.SpatialRel = geometryMapper.MapSpatialRelation(condition.RelationType);

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

                    feature = cursor.NextFeature();
                }

                result.TotalCount = result.Records.Count;
                result.Message = result.TotalCount == 0 ? "No matching features found." : string.Empty;
                return result;
            }
            catch (Exception ex)
            {
                throw ArcObjectsExceptionMapper.ToApplicationException("Spatial query failed.", ex);
            }
            finally
            {
                ArcObjectsComReleaser.Release(cursor);
            }
        }

        private IGeometry GetSelectedFeatureGeometry(string layerName, int objectId)
        {
            ICursor cursor = null;
            IFeatureLayer layer = FindFeatureLayer(layerName);
            if (layer == null || layer.FeatureClass == null)
            {
                return null;
            }

            if (objectId > 0)
            {
                IFeature featureById = layer.FeatureClass.GetFeature(objectId);
                return featureById == null ? null : featureById.ShapeCopy;
            }

            try
            {
                IFeatureSelection featureSelection = layer as IFeatureSelection;
                if (featureSelection == null || featureSelection.SelectionSet == null || featureSelection.SelectionSet.Count == 0)
                {
                    return null;
                }

                featureSelection.SelectionSet.Search(null, false, out cursor);
                IRow row = cursor == null ? null : cursor.NextRow();
                IFeature feature = row as IFeature;
                return feature == null ? null : feature.ShapeCopy;
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
    }
}
