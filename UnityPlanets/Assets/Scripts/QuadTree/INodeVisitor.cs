using JetBrains.Annotations;

namespace QuadTree
{
    public interface INodeVisitor
    {
        void AddVisited([NotNull] QuadTreeLeaf visitedLeaf);

    }
}
