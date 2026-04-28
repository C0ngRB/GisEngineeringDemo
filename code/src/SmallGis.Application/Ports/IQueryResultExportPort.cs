using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    public interface IQueryResultExportPort
    {
        void ExportToCsv(QueryResult result, string outputPath);
    }
}
