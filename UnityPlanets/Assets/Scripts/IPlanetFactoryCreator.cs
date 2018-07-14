using JetBrains.Annotations;

namespace Assets.Scripts
{
    public interface IPlanetFactoryCreator
    {
        [NotNull]
        IPlanetFactory CreatePlanetFactory();
    }
}
