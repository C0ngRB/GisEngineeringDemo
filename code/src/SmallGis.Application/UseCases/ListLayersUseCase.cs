using System;
using System.Collections.Generic;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    /// <summary>
    /// Lists current map layers without exposing ArcObjects to the UI. / 列出当前地图图层，同时不向 UI 暴露 ArcObjects。
    /// </summary>
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
