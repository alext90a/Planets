using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public class Camera : ICamera
    {
        private int mX;
        private int mY;
        private float mScale = 1f;

        public Camera()
        {
            mX = 0;
            mY = 0;
            Top = 100;
            Bottom = 0;
            Left = 0;
            Right = 100;
        }


        public int Top { get; }
        public int Left { get; }
        public int Bottom { get; }
        public int Right { get; }
        public void SetPosX(int x)
        {
            throw new NotImplementedException();
        }

        public void SetPosY(int y)
        {
            throw new NotImplementedException();
        }

        public void SetZoom(int zoom)
        {
            throw new NotImplementedException();
        }

        public int GetZoom()
        {
            throw new NotImplementedException();
        }

        
    }
}
