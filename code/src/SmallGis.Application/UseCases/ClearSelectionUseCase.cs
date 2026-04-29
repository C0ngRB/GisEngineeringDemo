using System;
using SmallGis.Application.Ports;

namespace SmallGis.Application.UseCases
{
    /// <summary>
    /// Clears map selection through the selection port. / 通过选择集端口清除地图选择。
    /// </summary>
    public class ClearSelectionUseCase
    {
        private readonly ISelectionPort selectionPort;

        public ClearSelectionUseCase(ISelectionPort selectionPort)
        {
            if (selectionPort == null) throw new ArgumentNullException("selectionPort");

            this.selectionPort = selectionPort;
        }

        public void Execute()
        {
            selectionPort.ClearSelection();
        }
    }
}
