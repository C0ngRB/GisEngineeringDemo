using System.Collections.Generic;

namespace SmallGis.Domain.Models
{
    /// <summary>
    /// Collection of feature records returned by a query use case. / 查询用例返回的要素记录集合。
    /// </summary>
    public class QueryResult
    {
        public QueryResult()
        {
            Records = new List<FeatureRecord>();
        }

        public string LayerName { get; set; }

        public int TotalCount { get; set; }

        public List<FeatureRecord> Records { get; set; }

        public string Message { get; set; }
    }
}
