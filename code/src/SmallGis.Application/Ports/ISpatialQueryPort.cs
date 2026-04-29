using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    /// <summary>
    /// Port for spatial queries; implementations own spatial filter details. / 空间查询端口；空间过滤细节由实现类负责。
    /// </summary>
    public interface ISpatialQueryPort
    {
        QueryResult QueryByExtent(SpatialQueryCondition condition);

        QueryResult QueryBySelectedFeature(SpatialQueryCondition condition);
    }
}
