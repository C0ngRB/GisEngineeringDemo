using SmallGis.Domain.Enums;

namespace SmallGis.Domain.Models
{
    /// <summary>
    /// Attribute query request. RawWhereClause takes precedence when supplied. / 属性查询请求；提供 RawWhereClause 时优先使用它。
    /// </summary>
    public class QueryCondition
    {
        public string LayerName { get; set; }

        public string FieldName { get; set; }

        public QueryOperator Operator { get; set; }

        public string Value { get; set; }

        public string RawWhereClause { get; set; }
    }
}
