using SmallGis.Domain.Enums;

namespace SmallGis.Domain.Models
{
    public class SpatialQueryCondition
    {
        public string TargetLayerName { get; set; }

        public SpatialRelationType RelationType { get; set; }

        public MapExtent QueryExtent { get; set; }

        public double BufferDistance { get; set; }

        public string SourceLayerName { get; set; }

        public int SourceFeatureObjectId { get; set; }
    }
}
