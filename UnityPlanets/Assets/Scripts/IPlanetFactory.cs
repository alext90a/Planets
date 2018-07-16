using JetBrains.Annotations;

public interface IPlanetFactory
{
    [NotNull]
    int[] CreatePlanetsForSector();
}