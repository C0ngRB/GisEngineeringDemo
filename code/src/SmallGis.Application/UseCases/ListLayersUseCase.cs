using System;
using System.Collections.Generic;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    public class ListLayersUseCase
    {
        private readonly ILayerCatalogPort layerCatalogPort;

        public ListLayersUseCase(ILayerCatalogPort layerCatalogPort)
        {
            if (layerCatalogPort == null) throw new ArgumentNullException("layerCatalogPort");

            this.layerCatalogPort = layerCatalogPort;
        }

        public IList<LayerInfo> Execute()
        {
            return layerCatalogPort.GetLayers();
        }
    }
}
