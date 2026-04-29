using SmallGis.Domain.Enums;

namespace SmallGis.Domain.Models
{
    /// <summary>
    /// Spatial query request expressed with domain types only. / 仅使用领域类型表达的空间查询请求。
    /// </summary>
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
