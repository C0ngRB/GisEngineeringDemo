using ESRI.ArcGIS.Geodatabase;
using SmallGis.Domain.Models;

namespace SmallGis.Infrastructure.ArcObjects.Mappers
{
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
