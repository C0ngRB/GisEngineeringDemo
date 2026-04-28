using System.Collections.Generic;
using SmallGis.Domain.Enums;

namespace SmallGis.Domain.Models
{
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
