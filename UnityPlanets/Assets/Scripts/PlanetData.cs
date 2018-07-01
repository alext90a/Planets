using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public struct PlanetData
    {
        private int mX;
        private int mY;
        private int mScore;

        public PlanetData(int x, int y, int score)
        {
            mX = x;
            mY = y;
            mScore = score;
        }

        public int X { get { return mX; } }
        public int Y { get { return mY; } }
        public int Score { get { return mScore; } }
    }
}
