using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Planets;

namespace Assets.Scripts
{
    public class PlanetFactory : IPlanetFactory
    {
        [NotNull]
        private readonly IConstants mConstants;

        [NotNull]
        private readonly Random mRandomGenerator;
        [NotNull]
        private readonly IComparer<int> mPlanetComparer;

        public PlanetFactory([NotNull]IConstants constants, [NotNull] Random randomGenerator, [NotNull] IComparer<int> planetComparer)
        {
            mConstants = constants;
            mRandomGenerator = randomGenerator;
            mPlanetComparer = planetComparer;
        }
        public int[] CreatePlanetsForSector()
        {
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
            return listRatings.ToArray();
            
        }

        private int GeneratePosition()
        {
            return mRandomGenerator.Next(0, mConstants.GetCellsInSector());
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        public int[] GetPlanetData()
        {
            return CreatePlanetsForSector();
        }
    }
}
