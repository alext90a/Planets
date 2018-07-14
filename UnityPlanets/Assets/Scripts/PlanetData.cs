namespace Assets.Scripts
{
    public struct PlanetData
    {
        private readonly float mX;
        private readonly float mY;
        private readonly int mScore;

        public PlanetData(float x, float y, int score)
        {
            mX = x;
            mY = y;
            mScore = score;
        }

        public float X { get { return mX; } }
        public float Y { get { return mY; } }
        public int Score { get { return mScore; } }
    }
}
