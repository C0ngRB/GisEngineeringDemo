namespace SmallGis.Domain.Models
{
    /// <summary>
    /// Serializable map extent represented without IEnvelope. / 可序列化的地图范围表示，不依赖 IEnvelope。
    /// </summary>
    public class MapExtent
    {
        public double XMin { get; set; }

        public double YMin { get; set; }

        public double XMax { get; set; }

        public double YMax { get; set; }
    }
}
