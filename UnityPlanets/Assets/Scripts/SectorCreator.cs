using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;

namespace Planets
{
    public class SectorCreator : ISectorCreator
    {
        [NotNull]
        private readonly IConstants mConstants;
        [NotNull]
        private readonly IPlanetFactory mPlanetFactory;

        public SectorCreator(IConstants constants, IPlanetFactory planetFactory)
        {
            mConstants = constants;
            mPlanetFactory = planetFactory;
        }

        public ISector CreateSector(int x, int y)
        {
            var sector = new Sector(mConstants, mPlanetFactory, x, y);

            return sector;
        }
    }
}
