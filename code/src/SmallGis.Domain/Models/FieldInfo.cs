namespace SmallGis.Domain.Models
{
    public class FieldInfo
    {
        public string Name { get; set; }

        public string AliasName { get; set; }

        public string FieldType { get; set; }

        public bool IsNullable { get; set; }

        public bool IsEditable { get; set; }
    }
}
