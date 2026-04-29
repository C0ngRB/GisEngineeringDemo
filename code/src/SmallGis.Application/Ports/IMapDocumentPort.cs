using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    /// <summary>
    /// Port for opening and saving map documents without exposing IMapDocument. / 打开和保存地图文档的端口，不暴露 IMapDocument。
    /// </summary>
    public interface IMapDocumentPort
    {
        MapDocumentInfo OpenMapDocument(string mxdPath);

        void SaveMapDocument(string mxdPath);

        bool CanOpen(string mxdPath);
    }
}
