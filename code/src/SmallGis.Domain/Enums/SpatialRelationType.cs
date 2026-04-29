namespace SmallGis.Domain.Enums
{
    /// <summary>
    /// Supported spatial relations for infrastructure-level spatial filters. / 基础设施层空间过滤器支持的空间关系。
    /// </summary>
    public enum SpatialRelationType
    {
        Intersects = 0,
        Contains = 1,
        Within = 2,
        EnvelopeIntersects = 3
    }
}
