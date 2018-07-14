using System;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;
using Planets;

namespace Editor.UnitTests
{
    public class CameraMock : ICamera
    {
        [NotNull] private Action<int, int> mPosChangedAction = (i, i1) => { };
        [NotNull] private Func<int> mGetTopFunc = () => 0;
        [NotNull] private Func<int> mGetLeftFunc = () => 0;
        [NotNull] private Func<int> mGetBottomFunc = () => 0;
        [NotNull] private Func<int> mGetRightFunc = () => 0;
        [NotNull] private Action mIncreaseFunc = () => { };
        [NotNull] private Action mDecreaseFunc = () => { };

        public void SetupTopFunc(Func<int> value)
        {
            mGetTopFunc = value;
        }

        public void SetupLeftFunc(Func<int> value)
        {
            mGetLeftFunc = value;
        }

        public void SetupBottomFunc(Func<int> value)
        {
            mGetBottomFunc = value;
        }

        public void SetupRightFunc(Func<int> value)
        {
            mGetRightFunc = value;
        }

        public void SetupIncreaseFunc(Action value)
        {
            mIncreaseFunc = value;
        }

        public void SetupDecreaseFunc(Action value)
        {
            mDecreaseFunc = value;
        }

        public void SetupPositionCanged([NotNull]Action<int, int> action)
        {
            mPosChangedAction = action;
        }



        public void PositionCanged(int posX, int posY)
        {
            mPosChangedAction.Invoke(posX, posY);
        }

        public int GetTop()
        {
            return mGetTopFunc.Invoke();
        }

        public int GetLeft()
        {
            return mGetLeftFunc.Invoke();
        }

        public int GetBottom()
        {
            return mGetBottomFunc.Invoke();
        }

        public int GetRight()
        {
            return mGetRightFunc.Invoke();
        }

        public void IncreaseZoom()
        {
            mIncreaseFunc.Invoke();
        }

        public void DecreaseZoom()
        {
            mDecreaseFunc.Invoke();
        }

        public int GetMaxZoom()
        {
            throw new System.NotImplementedException();
        }

        public int GetMinZoom()
        {
            throw new System.NotImplementedException();
        }

        public int GetZoom()
        {
            throw new System.NotImplementedException();
        }

        public void AddListener(ICameraListener listener)
        {
            throw new System.NotImplementedException();
        }

        public void AddBorderChangeListener(IBordersChangeListener listener)
        {
            throw new System.NotImplementedException();
        }

        public float GetX()
        {
            throw new NotImplementedException();
        }

        public float GetY()
        {
            throw new NotImplementedException();
        }

        public float GetWidth()
        {
            throw new NotImplementedException();
        }

        public float GetHeight()
        {
            throw new NotImplementedException();
        }

        public bool IsIntersect(IAABBox other)
        {
            throw new NotImplementedException();
        }
    }

}

