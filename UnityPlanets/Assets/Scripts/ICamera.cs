using Assets.Scripts;

namespace Planets
{
    public interface ICamera : IPlayerListener
    {
        int GetTop();
        int GetLeft();
        int GetBottom();
        int GetRight();
        void IncreaseZoom();
        void DecreaseZoom();
        int GetMaxZoom();
        int GetMinZoom();
        int GetZoom();
        void AddListener(ICameraListener listener);
        void AddBorderChangeListener(IBordersChangeListener listener);
    }
}
