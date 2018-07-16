using JetBrains.Annotations;

public interface IPlanetFactoryCreator
{
    [NotNull]
    IPlanetFactory CreatePlanetFactory();
}