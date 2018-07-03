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

        private int mPlayerX;
        private int mPlayerY;

        private List<ICameraListener> mListeners = new List<ICameraListener>();
        private List<IBordersChangeListener> mBorderListeners = new List<IBordersChangeListener>();

        public Camera()
        {
            mX = 0;
            mY = 0;
            mTop = 100;
            mBottom = 0;
            mLeft = 0;
            mRight = 100;
        }


        public int GetTop()
        {
            return mTop;
        }

        public int GetLeft()
        {
            return mLeft;
        }

        public int GetBottom()
        {
            return mBottom;
        }

        public int GetRight()
        {
            return mRight;
        }

        public void IncreaseZoom()
        {
            ++mZoomInd;
            if (mZoomInd >= mZoomValues.Count)
            {
                mZoomInd = mZoomValues.Count - 1;
                return;
            }

            UpdateListeners();
            UpdatePositions();
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
            UpdatePositions();
        }
        private void UpdateListeners()
        {
            int zoomValue = mZoomValues[mZoomInd];
            for (int i = 0; i < mListeners.Count; ++i)
            {
                mListeners[i].ZoomValueChanged(zoomValue);
            }
        }

        public int GetMaxZoom()
        {
            return mZoomValues[mZoomValues.Count - 1];
        }

        public int GetMinZoom()
        {
            return mZoomValues[0];
        }

        

        public int GetZoom()
        {
            return mZoomValues[mZoomInd];
        }

        public void AddListener(ICameraListener listener)
        {
            mListeners.Add(listener);
        }

        public void AddBorderChangeListener(IBordersChangeListener listener)
        {
            mBorderListeners.Add(listener);
        }


        public void PositionCanged(int posX, int posY)
        {
            mPlayerX = posX;
            mPlayerY = posY;

            UpdatePositions();
            
        }

        private void UpdatePositions()
        {
            var halfWidth = GetZoom() / 2;
            if (GetZoom() % 2 == 0)
            {
                mTop = mPlayerY + halfWidth;
                mBottom = mPlayerY - halfWidth + 1;
                mLeft = mPlayerX - halfWidth + 1;
                mRight = mPlayerX + halfWidth;
            }
            else
            {
                mTop = mPlayerY + halfWidth;
                mBottom = mPlayerY - halfWidth;
                mLeft = mPlayerX - halfWidth;
                mRight = mPlayerX + halfWidth;
            }

            for (int i = 0; i < mBorderListeners.Count; ++i)
            {
                mBorderListeners[i].NewBorders(mTop, mBottom, mLeft, mRight);
            }
        }
    }
}
