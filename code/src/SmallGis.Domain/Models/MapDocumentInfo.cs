using System.Collections.Generic;

namespace SmallGis.Domain.Models
{
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
