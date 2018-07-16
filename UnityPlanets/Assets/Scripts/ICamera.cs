using Assets.Scripts;
using Planets;
using QuadTree;

public interface ICamera : IPlayerListener, IAABBox
{
    void IncreaseZoom();
    void DecreaseZoom();
    int GetMaxZoom();
    int GetMinZoom();
    int GetZoom();
    void AddListener(ICameraListener listener);
    void AddBorderChangeListener(IBordersChangeListener listener);
}