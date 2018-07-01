using System.Collections.Generic;

namespace Planets
{
    public class SectorCreator : ISectorCreator
    {
        private readonly IConstants mConstants;
        private readonly IComparer<int> mComparer;

        public SectorCreator(IConstants constants, IComparer<int> comparer)
        {
            mConstants = constants;
            mComparer = comparer;
        }

        public ISector CreateSector(int x, int y)
        {
            var sector = new Sector(mConstants, mComparer);
            sector.Init();

            sector.GetY = x;
            sector.GetX = y;
            return sector;
        }
    }
}
