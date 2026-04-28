using System.Collections.Generic;
using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    public interface IFeatureQueryPort
    {
        QueryResult QueryByAttribute(QueryCondition condition);

        IList<FeatureRecord> GetAllFeatures(string layerName, int maxCount);
    }
}
