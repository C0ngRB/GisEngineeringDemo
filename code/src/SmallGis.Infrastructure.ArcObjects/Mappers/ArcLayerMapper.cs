using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using SmallGis.Domain.Enums;
using SmallGis.Domain.Models;

namespace SmallGis.Infrastructure.ArcObjects.Mappers
{
    public class ArcLayerMapper
    {
        private readonly ArcGeometryMapper geometryMapper;

        public ArcLayerMapper()
            : this(new ArcGeometryMapper())
        {
        }

        public ArcLayerMapper(ArcGeometryMapper geometryMapper)
        {
            this.geometryMapper = geometryMapper;
        }

        public LayerInfo Map(ILayer layer)
        {
            if (layer == null)
            {
                return null;
            }

            IFeatureLayer featureLayer = layer as IFeatureLayer;
            if (featureLayer != null && featureLayer.FeatureClass != null)
            {
                return MapFeatureLayer(featureLayer);
            }

            return new LayerInfo
            {
                Id = layer.Name,
                Name = layer.Name,
                LayerType = LayerType.Unknown,
                GeometryType = GeometryType.Unknown,
                Visible = layer.Visible,
                DataSourcePath = string.Empty,
                FeatureCount = 0
            };
        }

        private LayerInfo MapFeatureLayer(IFeatureLayer layer)
        {
            IFeatureClass featureClass = layer.FeatureClass;
            IDataset dataset = featureClass as IDataset;
            string dataSourcePath = string.Empty;
            if (dataset != null && dataset.Workspace != null)
            {
                dataSourcePath = dataset.Workspace.PathName;
            }

            return new LayerInfo
            {
                Id = layer.Name,
                Name = layer.Name,
                LayerType = LayerType.FeatureLayer,
                GeometryType = geometryMapper.MapGeometryType(featureClass.ShapeType),
                Visible = ((ILayer)layer).Visible,
                DataSourcePath = dataSourcePath,
                FeatureCount = featureClass.FeatureCount(null)
            };
        }
    }
}
