using SmallGis.Domain.Enums;

namespace SmallGis.Domain.Models
{
    public class QueryCondition
    {
        public string LayerName { get; set; }

        public string FieldName { get; set; }

        public QueryOperator Operator { get; set; }

        public string Value { get; set; }

        public string RawWhereClause { get; set; }
    }
}
