using System.Collections.Generic;

namespace SmallGis.Domain.Models
{
    /// <summary>
    /// Basic map document metadata returned after opening an MXD. / 打开 MXD 后返回的地图文档基础元数据。
    /// </summary>
    public class MapDocumentInfo
    {
        public MapDocumentInfo()
        {
            Layers = new List<LayerInfo>();
        }

        public string FilePath { get; set; }

        public string Title { get; set; }

        public List<LayerInfo> Layers { get; set; }
    }
}
