using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;

public interface IUnityPlanetVisualizer
{
    void Visualize([NotNull]List<PlanetData> planets);
}