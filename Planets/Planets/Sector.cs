using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public class Sector : ISector
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
                } while (allPositions.Contains(generatedPosition));
                var rating = mRandomGenerator.Next(mMinPlanetScore, mMaxPlanetScore);
                
                allPositions.Add(generatedPosition);
                
                listRatings.Add(rating * mSectorSize * mSectorSize + generatedPosition);
                if (rating == 10000)
                {
                    Segment.totalBig += 1;
                    Segment.bigs.Add(rating * mSectorSize * mSectorSize + generatedPosition);
                }
            }
            listRatings.Sort(Compare);
            mByRating = listRatings.ToArray();
            //mByRating[0] = 100009999;
        }

        private int GeneratePosition()
        {
            return mRandomGenerator.Next(0, cellsInSector);
        }

        public static int Compare(int x, int y)
        {
            if (x > y)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

    //public Planet[] mPlanetStore;
        public int[] mByRating;

        public int GetPlanet(int index)
        {
            return mByRating[index];
        }

        public int GetPlanetRating(int index)
        {
            return mByRating[index] / cellsInSector;
        }

        public int PlanetsInSector
        {
            get { return planetsInSector; }
        }

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
