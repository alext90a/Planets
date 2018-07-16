using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using QuadTree;

public interface IArrayBackgroundWorker
{
    void AddListener([NotNull] IArrayBackgroundWorkerListener listener);
    void Run([NotNull]IReadOnlyList<QuadTreeLeaf> loadingNodes, [NotNull]IPlanetFactoryCreator planetFactoryCreator);
}