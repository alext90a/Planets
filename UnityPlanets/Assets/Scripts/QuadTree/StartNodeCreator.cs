using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;

namespace QuadTree
{
    public sealed class StartNodeCreator : IStartNodeCreator
    {
        [NotNull]
        private readonly IConstants mConstants;
        [NotNull]
        private readonly IThreadQuadTreeNodeCreatorFactory mThreadQuadTreeNodeCreatorFactory;
        [NotNull]
        private readonly IQuadTreeNodeMerger mQuadTreeNodeMerger;
        
        public StartNodeCreator([NotNull] IConstants constants
            , [NotNull] IThreadQuadTreeNodeCreatorFactory threadNodeFactory
            , [NotNull] IQuadTreeNodeMerger quadTreeNodeMerger)
        {
            mConstants = constants;
            mThreadQuadTreeNodeCreatorFactory = threadNodeFactory;
            mQuadTreeNodeMerger = quadTreeNodeMerger;
        }

        public IQuadTreeNode Create()
        {
            var levels = 0;
            var segmentSize = mConstants.GetSectorSideSize();
            while (segmentSize < mConstants.GetMaxCameraSize())
            {
                segmentSize *= 2;
                ++levels;
            }
            IQuadTreeNode rootNode;

            if (levels > 0)
            {
                var allSubsectorsList = CreateQuadTreeNodesOnMultithreds(segmentSize);
                rootNode = mQuadTreeNodeMerger.Merge(allSubsectorsList);
            }
            else
            {
                rootNode = new QuadTreeLeaf(new AABBox(0f, 0f, segmentSize, segmentSize), mConstants);
            }
            return rootNode;
        }

        [NotNull]
        private List<IQuadTreeNode> CreateQuadTreeNodesOnMultithreds(int segmentSize)
        {
            var processorCount = Environment.ProcessorCount;
            var waitHandles = new WaitHandle[processorCount];
            var subsectors = 1;
            var subsectorSideSize = segmentSize;
            var subSectorsInRaw = 1;
            while (subsectors < processorCount)
            {
                subsectors *= 4;
                subSectorsInRaw *= 2;
                subsectorSideSize /= 2;
            }
            var sectorsInSegment = subSectorsInRaw * subsectorSideSize / mConstants.GetSectorSideSize();
            sectorsInSegment *= sectorsInSegment;
            var loadingNodes = new IQuadTreeNode[sectorsInSegment];
            var subsectorsPerProc = subsectors / processorCount;
            if (subsectors % processorCount != 0)
            {
                subsectorsPerProc += 1;
            }

            var threadQuadTreeNodeCreators = new IThreadQuadTreeNodeCreator[processorCount];
            var allSubsectors = new IQuadTreeNode[subsectors];
            for (int i = 0; i < processorCount; ++i)
            {
                var startIndex = i * subsectorsPerProc;
                var endIndex = startIndex + subsectorsPerProc;
                
                threadQuadTreeNodeCreators[i] = mThreadQuadTreeNodeCreatorFactory.Create(allSubsectors, loadingNodes, startIndex, endIndex, segmentSize
                    ,subsectorSideSize, subSectorsInRaw);
                waitHandles[i] = threadQuadTreeNodeCreators[i].GetWaitHandle();


                ThreadPool.QueueUserWorkItem(threadQuadTreeNodeCreators[i].GetWaitCallback());
            }
            
            WaitHandle.WaitAll(waitHandles);
            for (int i = 0; i < threadQuadTreeNodeCreators.Length; ++i)
            {
                // ReSharper disable once PossibleNullReferenceException
                var exception = threadQuadTreeNodeCreators[i].GetException();
                if (exception != null)
                {
                    throw exception;
                }
            }
            var allSubsectorsList = new List<IQuadTreeNode>(subsectors);
            allSubsectorsList.AddRange(allSubsectors);
            return allSubsectorsList;
        }
    }
}
