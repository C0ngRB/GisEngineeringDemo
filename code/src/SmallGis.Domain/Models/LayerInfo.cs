using SmallGis.Domain.Enums;

namespace SmallGis.Domain.Models
{
    /// <summary>
    /// ArcObjects-free description of a map layer for UI and use cases. / 面向界面和用例的图层描述，不包含 ArcObjects 对象。
    /// </summary>
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
