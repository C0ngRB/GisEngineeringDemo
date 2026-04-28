using System;
using ESRI.ArcGIS.Controls;
using SmallGis.Application.UseCases;
using SmallGis.Infrastructure.ArcObjects.Adapters;
using SmallGis.Infrastructure.ArcObjects.Logging;
using SmallGis.Presentation.WinForms.Controllers;

namespace SmallGis.Presentation.WinForms.Composition
{
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

            FileLogger logger = new FileLogger(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));
            ArcMapDocumentAdapter mapDocumentAdapter = new ArcMapDocumentAdapter(mapControlAdapter);
            ArcLayerCatalogAdapter layerCatalogAdapter = new ArcLayerCatalogAdapter(mapControlAdapter);
            ArcFeatureQueryAdapter featureQueryAdapter = new ArcFeatureQueryAdapter(mapControlAdapter);
            ArcSpatialQueryAdapter spatialQueryAdapter = new ArcSpatialQueryAdapter(mapControlAdapter);
            ArcSelectionAdapter selectionAdapter = new ArcSelectionAdapter(mapControlAdapter);
            ArcMapNavigationAdapter navigationAdapter = new ArcMapNavigationAdapter(mapControlAdapter);

            return new MainFormController(
                new OpenMapDocumentUseCase(mapDocumentAdapter, layerCatalogAdapter, logger),
                new AddShapefileLayerUseCase(layerCatalogAdapter, logger),
                new ListLayersUseCase(layerCatalogAdapter),
                new QueryFeaturesByAttributeUseCase(featureQueryAdapter, selectionAdapter, logger),
                new QueryFeaturesBySpatialRelationUseCase(spatialQueryAdapter, selectionAdapter, logger),
                new ClearSelectionUseCase(selectionAdapter),
                new ArcMapNavigationActions(navigationAdapter));
        }
    }
}
