using System;
using SmallGis.Application.Ports;

namespace SmallGis.Application.UseCases
{
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
