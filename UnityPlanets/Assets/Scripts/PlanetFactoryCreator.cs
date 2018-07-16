using System;
using JetBrains.Annotations;
using Planets;

public sealed class PlanetFactoryCreator : IPlanetFactoryCreator
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