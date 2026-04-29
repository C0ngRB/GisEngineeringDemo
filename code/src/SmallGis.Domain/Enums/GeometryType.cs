namespace SmallGis.Domain.Enums
{
    /// <summary>
    /// Domain-level geometry type used by query results and layer metadata. / 查询结果和图层元数据使用的领域层几何类型。
    /// </summary>
    public enum GeometryType
    {
        Unknown = 0,
        Point = 1,
        Polyline = 2,
        Polygon = 3
    }
}
