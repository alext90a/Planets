using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public struct Sector
    {

        public void Init()
        {
            mByRating = new int[planetsInSector];
            var allPositions = new HashSet<int>();
            var listRatings = new List<int>(planetsInSector);
            for (int i = 0; i < planetsInSector; ++i)
            {
                int generatedPosition;
                do
                {
                    generatedPosition = GeneratePosition();
                }
                while (allPositions.Contains(generatedPosition));
                var rating = mRandomGenerator.Next(mMinPlanetScore, mMaxPlanetScore);
                allPositions.Add(generatedPosition);
                listRatings.Add(rating * mSectorSize * mSectorSize + generatedPosition);
            }
            listRatings.Sort();
            mByRating = listRatings.ToArray();
        }

        private int GeneratePosition()
        {
            return mRandomGenerator.Next(0, cellsInSector);
        }


        //public Planet[] mPlanetStore;
        public int[] mByRating;

        public int GetX { get; set; }
        public int GetY { get; set; }
        //public int[] mByPositions;


        private const int planetsInSector = (int)(mSectorSize * mSectorSize * .3f);
        private const int cellsInSector = mSectorSize * mSectorSize;
        private const int mMinPlanetScore = 0;
        private const int mMaxPlanetScore = 10001;
        private const int mSectorSize = 100;

        private static Random mRandomGenerator = new Random();
    }
}
