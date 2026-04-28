using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using SmallGis.Domain.Enums;
using SmallGis.Domain.Models;

namespace SmallGis.Infrastructure.ArcObjects.Mappers
{
    public class ArcGeometryMapper
    {
        public GeometryType MapGeometryType(esriGeometryType geometryType)
        {
            switch (geometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    return GeometryType.Point;
                case esriGeometryType.esriGeometryPolyline:
                case esriGeometryType.esriGeometryLine:
                    return GeometryType.Polyline;
                case esriGeometryType.esriGeometryPolygon:
                    return GeometryType.Polygon;
                default:
                    return GeometryType.Unknown;
            }
        }

        public MapExtent MapEnvelope(IEnvelope envelope)
        {
            if (envelope == null)
            {
                return null;
            }

            return new MapExtent
            {
                XMin = envelope.XMin,
                YMin = envelope.YMin,
                XMax = envelope.XMax,
                YMax = envelope.YMax
            };
        }

        public IEnvelope ToEnvelope(MapExtent extent)
        {
            if (extent == null)
            {
                return null;
            }

            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(extent.XMin, extent.YMin, extent.XMax, extent.YMax);
            return envelope;
        }

        public esriSpatialRelEnum MapSpatialRelation(SpatialRelationType relationType)
        {
            switch (relationType)
            {
                case SpatialRelationType.Contains:
                    return esriSpatialRelEnum.esriSpatialRelContains;
                case SpatialRelationType.Within:
                    return esriSpatialRelEnum.esriSpatialRelWithin;
                case SpatialRelationType.EnvelopeIntersects:
                    return esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
                default:
                    return esriSpatialRelEnum.esriSpatialRelIntersects;
            }
        }
    }
}
