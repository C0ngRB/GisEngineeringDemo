using System.Collections.Generic;
using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    /// <summary>
    /// Port for feature reads and attribute queries. / 要素读取和属性查询端口。
    /// </summary>
    public interface IFeatureQueryPort
    {
        QueryResult QueryByAttribute(QueryCondition condition);

        IList<FeatureRecord> GetAllFeatures(string layerName, int maxCount);
    }
}
