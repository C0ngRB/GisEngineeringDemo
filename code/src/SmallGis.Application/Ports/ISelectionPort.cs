using System.Collections.Generic;

namespace SmallGis.Application.Ports
{
    public interface ISelectionPort
    {
        void SelectFeatures(string layerName, IList<int> objectIds);

        void ClearSelection();

        void FlashFeatures(string layerName, IList<int> objectIds);
    }
}
