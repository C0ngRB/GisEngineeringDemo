using System;
using System.Collections.Generic;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;
using SmallGis.Infrastructure.ArcObjects.Mappers;
using SmallGis.Infrastructure.ArcObjects.Utilities;
using DomainFieldInfo = SmallGis.Domain.Models.FieldInfo;

namespace SmallGis.Infrastructure.ArcObjects.Adapters
{
    public class ArcLayerCatalogAdapter : ILayerCatalogPort
    {
        private readonly IMapControl3 mapControl;
        private readonly ArcLayerMapper layerMapper;
        private readonly ArcFieldMapper fieldMapper;

        public ArcLayerCatalogAdapter(IMapControl3 mapControl)
            : this(mapControl, new ArcLayerMapper(), new ArcFieldMapper())
        {
        }

        public ArcLayerCatalogAdapter(IMapControl3 mapControl, ArcLayerMapper layerMapper, ArcFieldMapper fieldMapper)
        {
            if (mapControl == null) throw new ArgumentNullException("mapControl");

            this.mapControl = mapControl;
            this.layerMapper = layerMapper;
            this.fieldMapper = fieldMapper;
        }

        public IList<LayerInfo> GetLayers()
        {
            IList<LayerInfo> layers = new List<LayerInfo>();
            IMap map = mapControl.Map;
            if (map == null)
            {
                return layers;
            }

            for (int i = 0; i < map.LayerCount; i++)
            {
                LayerInfo layerInfo = layerMapper.Map(map.get_Layer(i));
                if (layerInfo != null)
                {
                    layers.Add(layerInfo);
                }
            }

            return layers;
        }

        public LayerInfo AddShapefile(string folderPath, string fileName)
        {
            IWorkspace workspace = null;
            try
            {
                if (string.IsNullOrWhiteSpace(folderPath) || string.IsNullOrWhiteSpace(fileName))
                {
                    throw new ArgumentException("Shapefile path is required.");
                }

                IWorkspaceFactory factory = new ShapefileWorkspaceFactoryClass();
                workspace = factory.OpenFromFile(folderPath, 0);
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(fileName);

                IFeatureLayer featureLayer = new FeatureLayerClass();
                featureLayer.FeatureClass = featureClass;
                featureLayer.Name = Path.GetFileNameWithoutExtension(fileName);

                mapControl.AddLayer((ILayer)featureLayer, 0);
                mapControl.ActiveView.Refresh();
                return layerMapper.Map((ILayer)featureLayer);
            }
            catch (Exception ex)
            {
                throw ArcObjectsExceptionMapper.ToApplicationException("Failed to add shapefile layer.", ex);
            }
            finally
            {
                ArcObjectsComReleaser.Release(workspace);
            }
        }

        public LayerInfo AddFileGdbFeatureClass(string gdbPath, string featureClassName)
        {
            IWorkspace workspace = null;
            try
            {
                if (string.IsNullOrWhiteSpace(gdbPath) || string.IsNullOrWhiteSpace(featureClassName))
                {
                    throw new ArgumentException("File geodatabase path and feature class name are required.");
                }

                IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
                workspace = factory.OpenFromFile(gdbPath, 0);
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
                IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(featureClassName);

                IFeatureLayer featureLayer = new FeatureLayerClass();
                featureLayer.FeatureClass = featureClass;
                featureLayer.Name = featureClassName;

                mapControl.AddLayer((ILayer)featureLayer, 0);
                mapControl.ActiveView.Refresh();
                return layerMapper.Map((ILayer)featureLayer);
            }
            catch (Exception ex)
            {
                throw ArcObjectsExceptionMapper.ToApplicationException("Failed to add FileGDB feature class.", ex);
            }
            finally
            {
                ArcObjectsComReleaser.Release(workspace);
            }
        }

        public void RemoveLayer(string layerName)
        {
            ILayer layer = FindLayer(layerName);
            if (layer == null)
            {
                throw new ArgumentException("Layer not found.", "layerName");
            }

            mapControl.Map.DeleteLayer(layer);
            mapControl.ActiveView.Refresh();
        }

        public void SetLayerVisible(string layerName, bool visible)
        {
            ILayer layer = FindLayer(layerName);
            if (layer == null)
            {
                throw new ArgumentException("Layer not found.", "layerName");
            }

            layer.Visible = visible;
            mapControl.ActiveView.Refresh();
        }

        public IList<DomainFieldInfo> GetFields(string layerName)
        {
            IList<DomainFieldInfo> fields = new List<DomainFieldInfo>();
            IFeatureLayer layer = FindFeatureLayer(layerName);
            if (layer == null || layer.FeatureClass == null)
            {
                throw new ArgumentException("Feature layer not found.", "layerName");
            }

            IFields arcFields = layer.FeatureClass.Fields;
            for (int i = 0; i < arcFields.FieldCount; i++)
            {
                DomainFieldInfo fieldInfo = fieldMapper.Map(arcFields.get_Field(i));
                if (fieldInfo != null)
                {
                    fields.Add(fieldInfo);
                }
            }

            return fields;
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

        private IFeatureLayer FindFeatureLayer(string layerName)
        {
            return FindLayer(layerName) as IFeatureLayer;
        }
    }
}
