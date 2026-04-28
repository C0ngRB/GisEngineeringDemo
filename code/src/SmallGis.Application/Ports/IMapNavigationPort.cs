namespace SmallGis.Application.Ports
{
    public interface IMapNavigationPort
    {
        void ZoomIn();

        void ZoomOut();

        void Pan();

        void FullExtent();

        void ZoomToLayer(string layerName);

        void ZoomToSelectedFeatures();
    }
}
