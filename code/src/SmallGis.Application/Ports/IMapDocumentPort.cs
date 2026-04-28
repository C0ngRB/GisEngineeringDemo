using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    public interface IMapDocumentPort
    {
        MapDocumentInfo OpenMapDocument(string mxdPath);

        void SaveMapDocument(string mxdPath);

        bool CanOpen(string mxdPath);
    }
}
