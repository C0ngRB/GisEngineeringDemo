using System;
using System.Collections.Generic;
using System.IO;
using SmallGis.Application.UseCases;
using SmallGis.Domain.Models;

namespace SmallGis.Presentation.WinForms.Controllers
{
    public class MainFormController
    {
        private readonly OpenMapDocumentUseCase openMapDocumentUseCase;
        private readonly AddShapefileLayerUseCase addShapefileLayerUseCase;
        private readonly ListLayersUseCase listLayersUseCase;
        private readonly QueryFeaturesByAttributeUseCase queryFeaturesByAttributeUseCase;
        private readonly QueryFeaturesBySpatialRelationUseCase queryFeaturesBySpatialRelationUseCase;
        private readonly ShowAttributeTableUseCase showAttributeTableUseCase;
        private readonly ExportQueryResultUseCase exportQueryResultUseCase;
        private readonly ClearSelectionUseCase clearSelectionUseCase;
        private readonly ArcMapNavigationActions navigationActions;

        public MainFormController(
            OpenMapDocumentUseCase openMapDocumentUseCase,
            AddShapefileLayerUseCase addShapefileLayerUseCase,
            ListLayersUseCase listLayersUseCase,
            QueryFeaturesByAttributeUseCase queryFeaturesByAttributeUseCase,
            QueryFeaturesBySpatialRelationUseCase queryFeaturesBySpatialRelationUseCase,
            ShowAttributeTableUseCase showAttributeTableUseCase,
            ExportQueryResultUseCase exportQueryResultUseCase,
            ClearSelectionUseCase clearSelectionUseCase,
            ArcMapNavigationActions navigationActions)
        {
            this.openMapDocumentUseCase = openMapDocumentUseCase;
            this.addShapefileLayerUseCase = addShapefileLayerUseCase;
            this.listLayersUseCase = listLayersUseCase;
            this.queryFeaturesByAttributeUseCase = queryFeaturesByAttributeUseCase;
            this.queryFeaturesBySpatialRelationUseCase = queryFeaturesBySpatialRelationUseCase;
            this.showAttributeTableUseCase = showAttributeTableUseCase;
            this.exportQueryResultUseCase = exportQueryResultUseCase;
            this.clearSelectionUseCase = clearSelectionUseCase;
            this.navigationActions = navigationActions;
        }

        public MapDocumentInfo OpenMapDocument(string mxdPath)
        {
            return openMapDocumentUseCase.Execute(mxdPath);
        }

        public LayerInfo AddShapefile(string shapefilePath)
        {
            if (string.IsNullOrWhiteSpace(shapefilePath))
            {
                throw new ArgumentException("Shapefile path is required.", "shapefilePath");
            }

            return addShapefileLayerUseCase.Execute(Path.GetDirectoryName(shapefilePath), Path.GetFileName(shapefilePath));
        }

        public IList<LayerInfo> ListLayers()
        {
            return listLayersUseCase.Execute();
        }

        public QueryResult QueryByAttribute(QueryCondition condition)
        {
            return queryFeaturesByAttributeUseCase.Execute(condition);
        }

        public QueryResult QueryBySpatialRelation(SpatialQueryCondition condition)
        {
            return queryFeaturesBySpatialRelationUseCase.Execute(condition);
        }

        public QueryResult ShowAttributeTable(string layerName, int maxCount)
        {
            return showAttributeTableUseCase.Execute(layerName, maxCount);
        }

        public void ExportQueryResult(QueryResult result, string outputPath)
        {
            exportQueryResultUseCase.Execute(result, outputPath);
        }

        public void ClearSelection()
        {
            clearSelectionUseCase.Execute();
        }

        public void FullExtent()
        {
            navigationActions.FullExtent();
        }

        public void ZoomIn()
        {
            navigationActions.ZoomIn();
        }

        public void ZoomOut()
        {
            navigationActions.ZoomOut();
        }

        public void Pan()
        {
            navigationActions.Pan();
        }
    }

    public class ArcMapNavigationActions
    {
        private readonly SmallGis.Application.Ports.IMapNavigationPort mapNavigationPort;

        public ArcMapNavigationActions(SmallGis.Application.Ports.IMapNavigationPort mapNavigationPort)
        {
            this.mapNavigationPort = mapNavigationPort;
        }

        public void FullExtent()
        {
            mapNavigationPort.FullExtent();
        }

        public void ZoomIn()
        {
            mapNavigationPort.ZoomIn();
        }

        public void ZoomOut()
        {
            mapNavigationPort.ZoomOut();
        }

        public void Pan()
        {
            mapNavigationPort.Pan();
        }
    }
}
