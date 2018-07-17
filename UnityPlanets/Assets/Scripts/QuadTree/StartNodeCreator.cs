using System;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using Planets;

namespace QuadTree
{
    public sealed class StartNodeCreator : IStartNodeCreator
    {
        [NotNull]
        private readonly IConstants mConstants;
        [NotNull]
        private static WaitHandle[] waitHandles;
        [NotNull]
        private static Exception[] exceptions;
        [NotNull]
        private IQuadTreeNode[] mLoadingNodes;

        
        public StartNodeCreator([NotNull] IConstants constants)
        {
            mConstants = constants;
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
                rootNode = new QuadTreeNodeMerger().Merge(allSubsectorsList);
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
            waitHandles = new WaitHandle[processorCount];
            exceptions = new Exception[processorCount];
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
            mLoadingNodes = new IQuadTreeNode[sectorsInSegment];
            var subsectorsPerProc = subsectors / processorCount;
            if (subsectors % processorCount != 0)
            {
                subsectorsPerProc += 1;
            }

            var allSubsectors = new IQuadTreeNode[subsectors];
            for (int i = 0; i < processorCount; ++i)
            {
                var startIndex = i * subsectorsPerProc;
                var endIndex = startIndex + subsectorsPerProc;
                var excIndex = i;
                waitHandles[i] = new AutoResetEvent(false);
                var waitCallback = new WaitCallback((Object state) =>
                {
                    AutoResetEvent are = (AutoResetEvent)state;
                    try
                    {
                        for (var j = startIndex; j < endIndex; ++j)
                        {
                            var posY = segmentSize / 2f - (j / subSectorsInRaw) * subsectorSideSize -
                                       subsectorSideSize / 2f;
                            var posX = -segmentSize / 2f + (j - subSectorsInRaw * (j / subSectorsInRaw)) * subsectorSideSize +
                                       subsectorSideSize / 2f;

                            var allLeafs = CreateStartLeafs(subsectorSideSize, posX, posY);

                            //
                            var startIndex1 = j * allLeafs.Count;
                            var endIndex1 = startIndex1 + allLeafs.Count;
                            var l = 0;
                            for (int k = startIndex1; k < endIndex1; ++k)
                            {
                                mLoadingNodes[k] = allLeafs[l];
                                ++l;
                            }

                            allSubsectors[j] = new QuadTreeNodeMerger().Merge(allLeafs);
                        }

                    }
                    catch (Exception e)
                    {
                        exceptions[excIndex] = e;
                    }
                    // ReSharper disable once PossibleNullReferenceException
                    are.Set();
                });
                ThreadPool.QueueUserWorkItem(waitCallback, waitHandles[i]);
                
            }
            
            WaitHandle.WaitAll(waitHandles);
            for (int i = 0; i < exceptions.Length; ++i)
            {
                if (exceptions[i] != null)
                {
                    throw exceptions[i];
                }
            }
            var allSubsectorsList = new List<IQuadTreeNode>(subsectors);
            allSubsectorsList.AddRange(allSubsectors);
            return allSubsectorsList;
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
