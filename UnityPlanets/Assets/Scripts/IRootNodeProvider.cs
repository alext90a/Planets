using JetBrains.Annotations;
using QuadTree;

public interface IRootNodeProvider
{
    [NotNull]
    IQuadTreeNode GetRootNote();
}