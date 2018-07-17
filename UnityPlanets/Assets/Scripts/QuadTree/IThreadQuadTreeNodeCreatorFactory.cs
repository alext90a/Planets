using JetBrains.Annotations;

namespace QuadTree
{
    public interface IThreadQuadTreeNodeCreatorFactory
    {
        [NotNull]
        IThreadQuadTreeNodeCreator Create([NotNull]IQuadTreeNode[] allSubsectors, [NotNull]IQuadTreeNode[] loadingNodes,
            int startIndex, int endIndex, int segmentSize, int subsectorSideSize, int subSectorsInRaw);
    }
}
