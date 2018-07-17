using System.Threading;
using JetBrains.Annotations;

namespace QuadTree
{
    public sealed class ThreadQuadTreeNodeCreatorFactory : IThreadQuadTreeNodeCreatorFactory
    {
        [NotNull]
        private readonly IConstants mConstants;

        public ThreadQuadTreeNodeCreatorFactory([NotNull] IConstants constants)
        {
            mConstants = constants;
        }

        public IThreadQuadTreeNodeCreator Create(IQuadTreeNode[] allSubsectors, IQuadTreeNode[] loadingNodes, 
            int startIndex, int endIndex, int segmentSize, int subsectorSideSize, int subSectorsInRaw)
        {
            return new ThreadQuadTreeNodeCreator(new AutoResetEvent(false)
                , new QuadTreeNodeMerger(), allSubsectors, mConstants, loadingNodes
                , startIndex, endIndex, segmentSize, subsectorSideSize, subSectorsInRaw);
        }
    }
}
