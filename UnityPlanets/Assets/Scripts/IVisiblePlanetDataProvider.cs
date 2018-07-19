using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using QuadTree;

public interface IVisiblePlanetDataProvider
{
    [NotNull]
    IReadOnlyList<PlanetData> GetVisiblePlanets([NotNull] IAABBox viewBBox);
}