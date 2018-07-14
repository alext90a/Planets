using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Planets;

namespace Assets.Scripts
{
    public class PlanetFactoryCreator : IPlanetFactoryCreator
    {
        [NotNull]
        private readonly IPlayer mPlayer;
        [NotNull]
        private readonly IConstants mConstants;

        public PlanetFactoryCreator([NotNull] IPlayer player, [NotNull] IConstants constants)
        {
            mPlayer = player;
            mConstants = constants;
        }


        public IPlanetFactory CreatePlanetFactory()
        {
            return new PlanetFactory(mConstants, new Random(), new PlanetComparer(mPlayer, mConstants));
        }
    }
}
