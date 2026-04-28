using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using SmallGis.Application.Ports;

namespace SmallGis.Infrastructure.ArcObjects.Adapters
{
    public class ArcMapNavigationAdapter : IMapNavigationPort
    {
        private readonly IMapControl3 mapControl;

        public ArcMapNavigationAdapter(IMapControl3 mapControl)
        {
            if (mapControl == null) throw new ArgumentNullException("mapControl");

            this.mapControl = mapControl;
        }

        public void ZoomIn()
        {
            ScaleExtent(0.5);
        }

        public void ZoomOut()
        {
            ScaleExtent(2.0);
        }

        public void Pan()
        {
            mapControl.MousePointer = esriControlsMousePointer.esriPointerPan;
        }

        public void FullExtent()
        {
            if (mapControl.FullExtent != null)
            {
                mapControl.Extent = mapControl.FullExtent;
                mapControl.ActiveView.Refresh();
            }
        }

        public void ZoomToLayer(string layerName)
        {
            ILayer layer = FindLayer(layerName);
            if (layer != null && layer.AreaOfInterest != null)
            {
                mapControl.Extent = layer.AreaOfInterest;
                mapControl.ActiveView.Refresh();
            }
        }

        public void ZoomToSelectedFeatures()
        {
            IEnumFeature selectedFeatures = mapControl.Map.FeatureSelection as IEnumFeature;
            if (selectedFeatures == null)
            {
                return;
            }

            selectedFeatures.Reset();
            IFeature feature = selectedFeatures.Next();
            IEnvelope envelope = null;
            while (feature != null)
            {
                if (feature.Shape != null)
                {
                    if (envelope == null)
                    {
                        envelope = feature.Shape.Envelope;
                    }
                    else
                    {
                        envelope.Union(feature.Shape.Envelope);
                    }
                }

                feature = selectedFeatures.Next();
            }

            if (envelope != null && !envelope.IsEmpty)
            {
                envelope.Expand(1.2, 1.2, true);
                mapControl.Extent = envelope;
                mapControl.ActiveView.Refresh();
            }
        }

        private void ScaleExtent(double factor)
        {
            IEnvelope envelope = mapControl.Extent;
            if (envelope == null || envelope.IsEmpty)
            {
                return;
            }

            envelope.Expand(factor, factor, true);
            mapControl.Extent = envelope;
            mapControl.ActiveView.Refresh();
        }

        private ILayer FindLayer(string layerName)
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
                    return layer;
                }
            }

            return null;
        }
    }
}
