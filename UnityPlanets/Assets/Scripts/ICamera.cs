using Assets.Scripts;

namespace Planets
{
    public interface ICamera : IPlayerListener
    {
        int Top { get; }
        int Left { get; }
        int Bottom { get; }
        int Right { get; }
        void IncreaseZoom();
        void DecreaseZoom();
        int GetMaxZoom();
        int GetMinZoom();
        int GetZoom();
        void AddListener(ICameraListener listener);
        void AddBorderChangeListener(IBordersChangeListener listener);
    }
}
