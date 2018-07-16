using System.Collections.Generic;
using JetBrains.Annotations;

namespace QuadTree
{
    public interface IQuadTreeNodeMerger
    {
        [NotNull]
        IQuadTreeNode Merge([NotNull] List<IQuadTreeNode> mergedNodes);
    }
}
