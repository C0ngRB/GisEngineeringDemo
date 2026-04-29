using System.Collections.Generic;

namespace SmallGis.Application.Ports
{
    /// <summary>
    /// Port for synchronizing query results with map selection. / 将查询结果同步到地图选择集的端口。
    /// </summary>
    public interface ISelectionPort
    {
        void SelectFeatures(string layerName, IList<int> objectIds);

        void ClearSelection();

        void FlashFeatures(string layerName, IList<int> objectIds);
    }
}
