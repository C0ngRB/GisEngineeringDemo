using ESRI.ArcGIS.Geodatabase;
using SmallGis.Domain.Models;

namespace SmallGis.Infrastructure.ArcObjects.Mappers
{
    /// <summary>
    /// Converts ArcObjects field metadata to a domain FieldInfo. / 将 ArcObjects 字段元数据转换为领域层 FieldInfo。
    /// </summary>
    public class ArcFieldMapper
    {
        public FieldInfo Map(IField field)
        {
            if (field == null)
            {
                return null;
            }

            return new FieldInfo
            {
                Name = field.Name,
                AliasName = field.AliasName,
                FieldType = field.Type.ToString(),
                IsNullable = field.IsNullable,
                IsEditable = field.Editable
            };
        }
    }
}
