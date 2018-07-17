using JetBrains.Annotations;

namespace QuadTree
{
    public interface IStartNodeCreator
    {
        [NotNull]
        IQuadTreeNode Create();
    }
}
