using System;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    public class AddShapefileLayerUseCase
    {
        private readonly ILayerCatalogPort layerCatalogPort;
        private readonly ILoggerPort logger;

        public AddShapefileLayerUseCase(ILayerCatalogPort layerCatalogPort, ILoggerPort logger)
        {
            if (layerCatalogPort == null) throw new ArgumentNullException("layerCatalogPort");
            if (logger == null) throw new ArgumentNullException("logger");

            this.layerCatalogPort = layerCatalogPort;
            this.logger = logger;
        }

        public LayerInfo Execute(string folderPath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("Shapefile folder path is required.", "folderPath");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Shapefile file name is required.", "fileName");
            }

            logger.Info("Add shapefile: " + folderPath + "\\" + fileName);
            return layerCatalogPort.AddShapefile(folderPath, fileName);
        }
    }
}
