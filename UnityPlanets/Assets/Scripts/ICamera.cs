using Assets.Scripts;

namespace Planets
{
    public interface ICamera : IPlayerListener
    {
        int Top { get; }
        int Left { get; }
        int Bottom { get; }
        int Right { get; }
        void SetPosX(int x);
        void SetPosY(int y);
        void SetZoom(int zoom);
        int GetZoom();
    }
}
