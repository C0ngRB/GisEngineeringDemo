namespace SmallGis.Domain.Models
{
    /// <summary>
    /// ArcObjects-free field metadata for query forms and attribute tables. / 面向查询窗体和属性表的字段元数据，不包含 ArcObjects 对象。
    /// </summary>
    public class FieldInfo
    {
        public string Name { get; set; }

        public string AliasName { get; set; }

        public string FieldType { get; set; }

        public bool IsNullable { get; set; }

        public bool IsEditable { get; set; }
    }
}
