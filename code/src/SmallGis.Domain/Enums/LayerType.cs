namespace SmallGis.Domain.Enums
{
    /// <summary>
    /// Describes the broad category of a map layer without exposing ArcObjects types. / 描述地图图层的大类，不暴露 ArcObjects 类型。
    /// </summary>
    public enum LayerType
    {
        Unknown = 0,
        FeatureLayer = 1,
        RasterLayer = 2
    }
}
