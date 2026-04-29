using System;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    /// <summary>
    /// Opens an MXD and refreshes layer metadata through application ports. / 通过应用层端口打开 MXD 并刷新图层元数据。
    /// </summary>
    public class OpenMapDocumentUseCase
    {
        private readonly IMapDocumentPort mapDocumentPort;
        private readonly ILayerCatalogPort layerCatalogPort;
        private readonly ILoggerPort logger;

        public OpenMapDocumentUseCase(
            IMapDocumentPort mapDocumentPort,
            ILayerCatalogPort layerCatalogPort,
            ILoggerPort logger)
        {
            if (mapDocumentPort == null) throw new ArgumentNullException("mapDocumentPort");
            if (layerCatalogPort == null) throw new ArgumentNullException("layerCatalogPort");
            if (logger == null) throw new ArgumentNullException("logger");

            this.mapDocumentPort = mapDocumentPort;
            this.layerCatalogPort = layerCatalogPort;
            this.logger = logger;
        }

        public MapDocumentInfo Execute(string mxdPath)
        {
            if (string.IsNullOrWhiteSpace(mxdPath))
            {
                throw new ArgumentException("Map document path is required.", "mxdPath");
            }

            logger.Info("Open MXD: " + mxdPath);

            MapDocumentInfo info = mapDocumentPort.OpenMapDocument(mxdPath);
            if (info != null)
            {
                info.Layers = new System.Collections.Generic.List<LayerInfo>(layerCatalogPort.GetLayers());
            }

            return info;
        }
    }
}
