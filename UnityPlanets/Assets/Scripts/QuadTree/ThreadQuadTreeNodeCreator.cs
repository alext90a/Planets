using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;

namespace QuadTree
{
    public sealed class ThreadQuadTreeNodeCreator : IThreadQuadTreeNodeCreator
    {
        [NotNull]
        private readonly AutoResetEvent mAutoResetEvent;
        [NotNull]
        private readonly IQuadTreeNodeMerger mNodeMerger;
        [CanBeNull]
        private Exception mException;
        [NotNull]
        private readonly IQuadTreeNode[] mAllSubsectors;//commmon for all threads
        [NotNull]
        private readonly IConstants mConstants;
        [NotNull]
        private readonly IQuadTreeNode[] mLoadingNodes;//commmon for all threads
        private readonly int mStartIndex;
        private readonly int mEndIndex;
        private readonly int mSegmentSize;
        private readonly int mSubsectorSideSize;
        private readonly int mSubSectorsInRaw;

        public ThreadQuadTreeNodeCreator([NotNull] AutoResetEvent resetEvent
            , [NotNull] IQuadTreeNodeMerger nodeMerger
            , [NotNull] IQuadTreeNode[] allSubsectors
            , [NotNull] IConstants constants
            , [NotNull] IQuadTreeNode[] loadingNodes
            , int startIndex
            , int endIndex
            , int segmentSize
            , int subsectorSideSize
            , int subSectorsInRaw)
            
        {
            mAutoResetEvent = resetEvent;
            mNodeMerger = nodeMerger;
            mAllSubsectors = allSubsectors;
            mConstants = constants;
            mStartIndex = startIndex;
            mEndIndex = endIndex;
            mSegmentSize = segmentSize;
            mSubsectorSideSize = subsectorSideSize;
            mSubSectorsInRaw = subSectorsInRaw;
            mLoadingNodes = loadingNodes;
        }

        public WaitCallback GetWaitCallback()
        {
            return Run;
        }

        public Exception GetException()
        {
            return mException;
        }
        
        public WaitHandle GetWaitHandle()
        {
            return mAutoResetEvent;
        }

        private void Run(Object o)
        {
            
            try
            {
                for (var j = mStartIndex; j < mEndIndex; ++j)
                {
                    var posY = mSegmentSize / 2f - (j / mSubSectorsInRaw) * mSubsectorSideSize -
                               mSubsectorSideSize / 2f;
                    var posX = -mSegmentSize / 2f + (j - mSubSectorsInRaw * (j / mSubSectorsInRaw)) * mSubsectorSideSize +
                               mSubsectorSideSize / 2f;

                    var allLeafs = CreateStartLeafs(mSubsectorSideSize, posX, posY);

                    //
                    var startIndex1 = j * allLeafs.Count;
                    var endIndex1 = startIndex1 + allLeafs.Count;
                    var l = 0;
                    for (int k = startIndex1; k < endIndex1; ++k)
                    {
                        mLoadingNodes[k] = allLeafs[l];
                        ++l;
                    }

                    mAllSubsectors[j] = mNodeMerger.Merge(allLeafs);
                }

            }
            catch (Exception e)
            {
                mException = e;
            }
            mAutoResetEvent.Set();
        }

        [NotNull]
        private List<IQuadTreeNode> CreateStartLeafs(int segmentSideSize, float segmentX, float segmentY)
        {
            var segmentsRaw = segmentSideSize / mConstants.GetSectorSideSize();
            var segmentsInSector = segmentsRaw * segmentsRaw;
            var sectorStore = new List<IQuadTreeNode>(segmentsInSector);
            var segmentHalfSize = segmentSideSize / 2f;
            var sectorHalfSize = mConstants.GetSectorSideSize() / 2f;
            for (int i = 0; i < segmentsInSector; ++i)
            {
                // ReSharper disable once PossibleLossOfFraction
                float y = i / segmentsRaw;
                float x = i - y * segmentsRaw;
                x = segmentX - segmentHalfSize + x * mConstants.GetSectorSideSize() + sectorHalfSize;
                y = segmentY + segmentHalfSize - y * mConstants.GetSectorSideSize() - sectorHalfSize;

                var box = new AABBox(x, y, mConstants.GetSectorSideSize(), mConstants.GetSectorSideSize());
                sectorStore.Add(new QuadTreeLeaf(box, mConstants));
            }
            return sectorStore;
        }


    }
}
