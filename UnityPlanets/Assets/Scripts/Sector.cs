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

        public PlanetData GetPlanetData(int index)
        {
            var data = mByRating[index];
            var score = data / mConstants.GetCellsInSector();
            var posLocal = data - score * mConstants.GetMaxPlanetScore();
            var posLocY = posLocal / mConstants.GetSectorSideSize();
            var posLocX = posLocal - posLocY * mConstants.GetSectorSideSize();
            return new PlanetData(mX * mConstants.GetSectorSideSize() + posLocX, mY * mConstants.GetSectorSideSize() + posLocY, score);
        }
    }
}
