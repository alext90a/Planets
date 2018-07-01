using System;
using Assets.Scripts;

namespace Planets
{
    public class Camera : ICamera
    {
        private int mX;
        private int mY;
        private float mScale = 1f;

        private int mTop;
        private int mBottom;
        private int mLeft;
        private int mRight;

        public Camera()
        {
            mX = 0;
            mY = 0;
            mTop = 100;
            mBottom = 0;
            mLeft = 0;
            mRight = 100;
        }


        public int Top { get { return mTop; } }
        public int Left { get { return mLeft; } }
        public int Bottom { get { return mBottom; } }
        public int Right { get { return mRight; } }
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


        public void PositionCanged(int posX, int posY)
        {
            
        }
    }
}
