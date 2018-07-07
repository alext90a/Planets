using System;
using System.Collections.Generic;
using Boo.Lang.Environments;

namespace Planets
{
    public class Sector : ISector
    {
        private readonly IConstants mConstants;
        private readonly IComparer<int> mPlanetComparer;
        private readonly int mX;
        private readonly int mY;

        public Sector(IConstants constants, IComparer<int> planetComparer, int x, int y)
        {
            mConstants = constants;
            mPlanetComparer = planetComparer;
            mX = x;
            mY = y;
        }

        public void Init()
        {
            mByRating = new int[mConstants.GetPlanetsInSector()];
            var allPositions = new HashSet<int>();
            var listRatings = new List<int>(mConstants.GetPlanetsInSector());
            for (int i = 0; i < mConstants.GetPlanetsInSector(); ++i)
            {
                int generatedPosition;
                do
                {
                    generatedPosition = GeneratePosition();
                } while (allPositions.Contains(generatedPosition));
                var rating = mRandomGenerator.Next(mConstants.GetMinPlanetScore(), mConstants.GetMaxPlanetScore() + 1);

                allPositions.Add(generatedPosition);

                listRatings.Add(rating * mConstants.GetCellsInSector() + generatedPosition);
            }
            listRatings.Sort(mPlanetComparer);
            mByRating = listRatings.ToArray();
        }

        private int GeneratePosition()
        {
            return mRandomGenerator.Next(0, mConstants.GetCellsInSector());
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
            return mByRating[index] / mConstants.GetCellsInSector();
        }




        public int GetX()
        {
            return mX;
        }

        public int GetY()
        {
            return mY;
        }



        private Random mRandomGenerator = new Random();
    }
}
