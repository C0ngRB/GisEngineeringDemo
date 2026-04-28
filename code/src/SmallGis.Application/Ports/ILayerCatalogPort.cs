using System.Collections.Generic;
using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    public interface ILayerCatalogPort
    {
        IList<LayerInfo> GetLayers();

        LayerInfo AddShapefile(string folderPath, string fileName);

        LayerInfo AddFileGdbFeatureClass(string gdbPath, string featureClassName);

        void RemoveLayer(string layerName);

        void SetLayerVisible(string layerName, bool visible);

        IList<FieldInfo> GetFields(string layerName);
    }
}
