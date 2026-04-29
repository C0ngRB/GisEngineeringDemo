using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    /// <summary>
    /// Port for exporting query results without tying use cases to file format code. / 查询结果导出端口，避免用例绑定具体文件格式代码。
    /// </summary>
    public interface IQueryResultExportPort
    {
        void ExportToCsv(QueryResult result, string outputPath);
    }
}
