using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    public interface ISpatialQueryPort
    {
        QueryResult QueryByExtent(SpatialQueryCondition condition);

        QueryResult QueryBySelectedFeature(SpatialQueryCondition condition);
    }
}
