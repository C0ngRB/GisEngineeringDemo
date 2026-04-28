using System.Collections.Generic;

namespace SmallGis.Domain.Models
{
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
