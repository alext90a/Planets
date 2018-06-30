using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (Math.Abs(x/mConstants.MaxPlanetScore - mPlayerScore) > Math.Abs(y/mConstants.MaxPlanetScore - mPlayerScore))
            {
                return 1;
            }
            return -1;
        }
    }
}
