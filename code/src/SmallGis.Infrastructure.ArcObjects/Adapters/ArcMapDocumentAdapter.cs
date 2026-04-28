using System;
using System.IO;
using ESRI.ArcGIS.Controls;
using SmallGis.Application.Ports;
using SmallGis.Domain.Models;
using SmallGis.Infrastructure.ArcObjects.Utilities;

namespace SmallGis.Infrastructure.ArcObjects.Adapters
{
    public class ArcMapDocumentAdapter : IMapDocumentPort
    {
        private readonly IMapControl3 mapControl;

        public ArcMapDocumentAdapter(IMapControl3 mapControl)
        {
            if (mapControl == null) throw new ArgumentNullException("mapControl");

            this.mapControl = mapControl;
        }

        public MapDocumentInfo OpenMapDocument(string mxdPath)
        {
            try
            {
                if (!CanOpen(mxdPath))
                {
                    throw new ArgumentException("Map document cannot be opened.", "mxdPath");
                }

                mapControl.LoadMxFile(mxdPath, null, null);

                return new MapDocumentInfo
                {
                    FilePath = mxdPath,
                    Title = Path.GetFileNameWithoutExtension(mxdPath)
                };
            }
            catch (Exception ex)
            {
                throw ArcObjectsExceptionMapper.ToApplicationException("Failed to open map document.", ex);
            }
        }

        public void SaveMapDocument(string mxdPath)
        {
            throw new NotSupportedException("Saving MXD is not implemented in the initial adapter.");
        }

        public bool CanOpen(string mxdPath)
        {
            return !string.IsNullOrWhiteSpace(mxdPath) &&
                   File.Exists(mxdPath) &&
                   mapControl.CheckMxFile(mxdPath);
        }
    }
}
