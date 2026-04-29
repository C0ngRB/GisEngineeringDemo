using System.Collections.Generic;
using SmallGis.Domain.Models;

namespace SmallGis.Application.Ports
{
    /// <summary>
    /// Port for layer catalog operations implemented by the ArcObjects adapter. / 图层目录操作端口，由 ArcObjects 适配器实现。
    /// </summary>
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
