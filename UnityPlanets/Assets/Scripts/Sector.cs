using System;
using Assets.Scripts;
using JetBrains.Annotations;

namespace Planets
{
    public class Sector : ISector
    {
        [NotNull]
        private readonly IConstants mConstants;
        [NotNull]
        private int[] mByRating;

        private readonly int mX;
        private readonly int mY;

        public Sector([NotNull]IConstants constants, [NotNull]IPlanetFactory planetFactory, int x, int y)
        {
            mConstants = constants;
            mX = x;
            mY = y;
            mByRating = planetFactory.CreatePlanetsForSector(mConstants.GetPlanetsInSector());
        }

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
    }
}
