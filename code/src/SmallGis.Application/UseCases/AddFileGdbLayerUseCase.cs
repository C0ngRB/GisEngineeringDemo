using System;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;

namespace SmallGis.Application.UseCases
{
    public class AddFileGdbLayerUseCase
    {
        private readonly ILayerCatalogPort layerCatalogPort;
        private readonly ILoggerPort logger;

        public AddFileGdbLayerUseCase(ILayerCatalogPort layerCatalogPort, ILoggerPort logger)
        {
            if (layerCatalogPort == null) throw new ArgumentNullException("layerCatalogPort");
            if (logger == null) throw new ArgumentNullException("logger");

            this.layerCatalogPort = layerCatalogPort;
            this.logger = logger;
        }

        public LayerInfo Execute(string gdbPath, string featureClassName)
        {
            if (string.IsNullOrWhiteSpace(gdbPath))
            {
                throw new ArgumentException("File Geodatabase path is required.", "gdbPath");
            }

            if (string.IsNullOrWhiteSpace(featureClassName))
            {
                throw new ArgumentException("Feature class name is required.", "featureClassName");
            }

            logger.Info("Add FileGDB feature class: " + gdbPath + "\\" + featureClassName);
            return layerCatalogPort.AddFileGdbFeatureClass(gdbPath, featureClassName);
        }
    }
}
