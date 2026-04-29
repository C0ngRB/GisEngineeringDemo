using System.Collections.Generic;
using SmallGis.Domain.Enums;

namespace SmallGis.Domain.Models
{
    /// <summary>
    /// Query result row converted from an ArcObjects feature. / 由 ArcObjects 要素转换得到的查询结果行。
    /// </summary>
    public class FeatureRecord
    {
        public FeatureRecord()
        {
            Attributes = new Dictionary<string, object>();
        }

        public int ObjectId { get; set; }

        public string LayerName { get; set; }

        public GeometryType GeometryType { get; set; }

        public MapExtent Extent { get; set; }

        public Dictionary<string, object> Attributes { get; set; }
    }
}
