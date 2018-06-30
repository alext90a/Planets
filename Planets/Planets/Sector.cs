using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public class Sector : ISector
    {
        private readonly IConstants mConstants;
        private readonly IComparer<int> mPlanetComparer;

        public Sector(IConstants constants, IComparer<int> planetComparer)
        {
            mConstants = constants;
            mPlanetComparer = planetComparer;
        }

        public void Init()
        {
            mByRating = new int[mConstants.PlanetsInSector];
            var allPositions = new HashSet<int>();
            var listRatings = new List<int>(mConstants.PlanetsInSector);
            for (int i = 0; i < mConstants.PlanetsInSector; ++i)
            {
                int generatedPosition;
                do
                {
                    generatedPosition = GeneratePosition();
                } while (allPositions.Contains(generatedPosition));
                var rating = mRandomGenerator.Next(mConstants.MinPlanetScore, mConstants.MaxPlanetScore+1);
                
                allPositions.Add(generatedPosition);
                
                listRatings.Add(rating * mConstants.CellsInSector + generatedPosition);
                if (rating == 10000)
                {
                    Segment.totalBig += 1;
                    Segment.bigs.Add(rating * mConstants.CellsInSector + generatedPosition);
                }
            }
            listRatings.Sort(mPlanetComparer);
            mByRating = listRatings.ToArray();
            //mByRating[0] = 100009999;
        }

        private int GeneratePosition()
        {
            return mRandomGenerator.Next(0, mConstants.CellsInSector);
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
            return mByRating[index] / mConstants.CellsInSector;
        }


        

        public int GetX { get; set; }
        public int GetY { get; set; }

        private static Random mRandomGenerator = new Random();
    }
}
