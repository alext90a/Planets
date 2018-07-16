using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Planets
{
    public class PlanetComparer : IComparer<int>
    {
        [NotNull]
        private readonly int mPlayerScore;
        [NotNull]
        private readonly IConstants mConstants;

        public PlanetComparer([NotNull]IPlayer player, [NotNull]IConstants constants)
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
