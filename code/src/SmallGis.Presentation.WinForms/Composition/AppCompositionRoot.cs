using System;
using ESRI.ArcGIS.Controls;
using SmallGis.Application.UseCases;
using SmallGis.Infrastructure.ArcObjects.Adapters;
using SmallGis.Infrastructure.ArcObjects.Export;
using SmallGis.Infrastructure.ArcObjects.Logging;
using SmallGis.Presentation.WinForms.Controllers;

namespace SmallGis.Presentation.WinForms.Composition
{
    /// <summary>
    /// Builds the object graph for the WinForms application. / 构建 WinForms 应用的对象依赖图。
    /// </summary>
    public static class AppCompositionRoot
    {
        public static MainFormController Create(AxMapControl mapControl)
        {
            if (mapControl == null) throw new ArgumentNullException("mapControl");

            IMapControl3 mapControlAdapter = mapControl.Object as IMapControl3;
            if (mapControlAdapter == null)
            {
                throw new InvalidOperationException("ArcGIS map control is not initialized.");
            }

            // ArcGIS controls stay in Presentation; adapters receive only the ArcObjects control interface. / ArcGIS 控件保留在 Presentation，适配器只接收 ArcObjects 控件接口。
            FileLogger logger = new FileLogger(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));
            ArcMapDocumentAdapter mapDocumentAdapter = new ArcMapDocumentAdapter(mapControlAdapter);
            ArcLayerCatalogAdapter layerCatalogAdapter = new ArcLayerCatalogAdapter(mapControlAdapter);
            ArcFeatureQueryAdapter featureQueryAdapter = new ArcFeatureQueryAdapter(mapControlAdapter);
            ArcSpatialQueryAdapter spatialQueryAdapter = new ArcSpatialQueryAdapter(mapControlAdapter);
            ArcSelectionAdapter selectionAdapter = new ArcSelectionAdapter(mapControlAdapter);
            ArcMapNavigationAdapter navigationAdapter = new ArcMapNavigationAdapter(mapControlAdapter);
            CsvQueryResultExporter csvExporter = new CsvQueryResultExporter();

            return new MainFormController(
                new OpenMapDocumentUseCase(mapDocumentAdapter, layerCatalogAdapter, logger),
                new AddShapefileLayerUseCase(layerCatalogAdapter, logger),
                new ListLayersUseCase(layerCatalogAdapter),
                new QueryFeaturesByAttributeUseCase(featureQueryAdapter, selectionAdapter, logger),
                new QueryFeaturesBySpatialRelationUseCase(spatialQueryAdapter, selectionAdapter, logger),
                new ShowAttributeTableUseCase(featureQueryAdapter, logger),
                new ExportQueryResultUseCase(csvExporter, logger),
                new ClearSelectionUseCase(selectionAdapter),
                new ArcMapNavigationActions(navigationAdapter));
        }
    }
}
