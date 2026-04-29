namespace SmallGis.Application.Ports
{
    /// <summary>
    /// Port for map navigation actions exposed to UI controllers. / 暴露给 UI 控制器的地图浏览操作端口。
    /// </summary>
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
