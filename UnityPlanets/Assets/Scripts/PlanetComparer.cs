using System;
using System.Collections.Generic;

namespace Planets
{
    public class PlanetComparer : IComparer<int>
    {
        private readonly int mPlayerScore;
        private readonly IConstants mConstants;

        public PlanetComparer(IPlayer player, IConstants constants)
        {
            mPlayerScore = player.Score;
            mConstants = constants;
        }

        public int Compare(int x, int y)
        {
            if (Math.Abs(x/mConstants.GetMaxPlanetScore() - mPlayerScore) >= Math.Abs(y/mConstants.GetMaxPlanetScore() - mPlayerScore))
            {
                return 1;
            }
            return -1;
        }
    }
}
