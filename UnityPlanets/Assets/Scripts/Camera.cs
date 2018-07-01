using System;
using Assets.Scripts;
using Boo.Lang;

namespace Planets
{
    public class Camera : ICamera
    {
        private int mX;
        private int mY;
        private float mScale = 1f;
        private List<int> mZoomValues = new List<int>(){5, 10, 100, 1000, 10000};
        private int mZoomInd = 0;

        private int mTop;
        private int mBottom;
        private int mLeft;
        private int mRight;

        private List<ICameraListener> mListeners = new List<ICameraListener>();

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
        public void IncreaseZoom()
        {
            ++mZoomInd;
            if (mZoomInd >= mZoomValues.Count)
            {
                mZoomInd = mZoomValues.Count - 1;
                return;
            }
            UpdateListeners();
        }

        public void DecreaseZoom()
        {
            --mZoomInd;
            if (mZoomInd < 0)
            {
                mZoomInd = 0;
                return;
            }
            UpdateListeners();
        }

        public int GetMaxZoom()
        {
            return mZoomValues[mZoomValues.Count - 1];
        }

        public int GetMinZoom()
        {
            return mZoomValues[0];
        }

        private void UpdateListeners()
        {
            int zoomValue = mZoomValues[mZoomInd];
            for (int i = 0; i < mListeners.Count; ++i)
            {
                mListeners[i].ZoomValueChanged(zoomValue);
            }
        }

        public int GetZoom()
        {
            return mZoomValues[mZoomInd];
        }

        public void AddListener(ICameraListener listener)
        {
            mListeners.Add(listener);
        }


        public void PositionCanged(int posX, int posY)
        {
            
        }
    }
}
