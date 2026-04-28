using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using SmallGis.Application.Ports;
using SmallGis.Infrastructure.ArcObjects.Utilities;

namespace SmallGis.Infrastructure.ArcObjects.Adapters
{
    public class ArcSelectionAdapter : ISelectionPort
    {
        private readonly IMapControl3 mapControl;

        public ArcSelectionAdapter(IMapControl3 mapControl)
        {
            if (mapControl == null) throw new ArgumentNullException("mapControl");

            this.mapControl = mapControl;
        }

        public void SelectFeatures(string layerName, IList<int> objectIds)
        {
            try
            {
                IFeatureLayer layer = FindFeatureLayer(layerName);
                if (layer == null || layer.FeatureClass == null)
                {
                    throw new ArgumentException("Feature layer not found.", "layerName");
                }

                IFeatureSelection featureSelection = (IFeatureSelection)layer;
                featureSelection.Clear();

                if (objectIds != null)
                {
                    for (int i = 0; i < objectIds.Count; i++)
                    {
                        IFeature feature = layer.FeatureClass.GetFeature(objectIds[i]);
                        if (feature != null)
                        {
                            featureSelection.Add(feature);
                        }
                    }
                }

                mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
            catch (Exception ex)
            {
                throw ArcObjectsExceptionMapper.ToApplicationException("Feature selection failed.", ex);
            }
        }

        public void ClearSelection()
        {
            if (mapControl.Map != null)
            {
                mapControl.Map.ClearSelection();
                mapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            }
        }

        public void FlashFeatures(string layerName, IList<int> objectIds)
        {
            SelectFeatures(layerName, objectIds);
        }

        private IFeatureLayer FindFeatureLayer(string layerName)
        {
            IMap map = mapControl.Map;
            if (map == null)
            {
                return null;
            }

            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer != null && string.Equals(layer.Name, layerName, StringComparison.OrdinalIgnoreCase))
                {
                    return layer as IFeatureLayer;
                }
            }

            return null;
        }
    }
}
