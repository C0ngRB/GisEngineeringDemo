using SmallGis.Domain.Enums;

namespace SmallGis.Domain.Models
{
    public class LayerInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public LayerType LayerType { get; set; }

        public GeometryType GeometryType { get; set; }

        public bool Visible { get; set; }

        public string DataSourcePath { get; set; }

        public int FeatureCount { get; set; }
    }
}
