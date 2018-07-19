using System.Collections.Generic;
using JetBrains.Annotations;

namespace QuadTree
{
    public interface INodeVisitor
    {
        void AddVisited([NotNull] QuadTreeLeaf visitedLeaf);
        void Clear();
        [NotNull]
        IReadOnlyList<QuadTreeLeaf> GetVisibleLeaves();
    }
}
