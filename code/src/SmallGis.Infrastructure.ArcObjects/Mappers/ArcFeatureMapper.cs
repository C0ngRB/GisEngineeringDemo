using ESRI.ArcGIS.Geodatabase;
using SmallGis.Domain.Models;

namespace SmallGis.Infrastructure.ArcObjects.Mappers
{
    public class ArcFeatureMapper
    {
        private readonly ArcGeometryMapper geometryMapper;

        public ArcFeatureMapper()
            : this(new ArcGeometryMapper())
        {
        }

        public ArcFeatureMapper(ArcGeometryMapper geometryMapper)
        {
            this.geometryMapper = geometryMapper;
        }

        public FeatureRecord Map(IFeature feature, string layerName)
        {
            if (feature == null)
            {
                return null;
            }

            FeatureRecord record = new FeatureRecord
            {
                ObjectId = feature.OID,
                LayerName = layerName,
                GeometryType = geometryMapper.MapGeometryType(feature.Shape.GeometryType),
                Extent = feature.Shape == null ? null : geometryMapper.MapEnvelope(feature.Shape.Envelope)
            };

            IFields fields = feature.Fields;
            for (int i = 0; i < fields.FieldCount; i++)
            {
                IField field = fields.get_Field(i);
                if (field == null || field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    continue;
                }

                object value = feature.get_Value(i);
                record.Attributes[field.Name] = value;
            }

            return record;
        }
    }
}
