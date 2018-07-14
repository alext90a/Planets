using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boo.Lang.Environments;

namespace Assets.Scripts.QuadTree
{
    public sealed class AABBox : IAABBox
    {
        private readonly float mX;
        private readonly float mY;
        private readonly float mWidth;
        private readonly float mHeight;

        public AABBox(float x, float y, float width, float height)
        {
            mX = x;
            mY = y;
            mWidth = width;
            mHeight = height;
        }

        public AABBox(int left, int top, int right, int bottom)
        {
            mX = left + (right - left) / 2f;
            mY = bottom + (top - bottom) / 2f;
            mWidth = right - left;
            mHeight = top - bottom;
        }

        public float GetX()
        {
            return mX;
        }

        public float GetY()
        {
            return mY;
        }

        public float GetWidth()
        {
            return mWidth;
        }

        public float GetHeight()
        {
            return mHeight;
        }

        public bool IsIntersect(IAABBox other)
        {
            return (Math.Abs(mX - other.GetX()) * 2 < (mWidth + other.GetWidth())) &&
                   (Math.Abs(mY - other.GetY()) * 2 < (mHeight + other.GetHeight()));
        }

    }
}
