using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public interface ICamera
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
