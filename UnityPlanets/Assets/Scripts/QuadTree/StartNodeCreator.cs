using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;
using Planets;

namespace QuadTree
{
    public sealed class StartNodeCreator
    {
        [NotNull] private readonly IConstants mConstants;
        [NotNull] private readonly IPlayer mPlayer;
        [NotNull] private readonly IPlanetFactoryCreator mPlanetFactoryCreator;
        [NotNull]
        private static WaitHandle[] waitHandles;
        public StartNodeCreator([NotNull] IConstants constants, [NotNull] IPlayer player, [NotNull] IPlanetFactoryCreator planetFactoryCreator)
        {
            mConstants = constants;
            mPlayer = player;
            mPlanetFactoryCreator = planetFactoryCreator;
        }

        [NotNull]
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

            var planetDataProvider = mPlanetFactoryCreator.CreatePlanetFactory();
            if (levels > 0)
            {
                var allSubsectorsList = CreateQuadTreeNodesOnMultithreds(segmentSize);
                rootNode = new QuadTreeNodeMerger().Merge(allSubsectorsList);
            }
            else
            {
                rootNode = new QuadTreeLeaf(new AABBox(0f, 0f, segmentSize, segmentSize),  planetDataProvider.CreatePlanetsForSector(), mConstants, mPlayer);
            }
            return rootNode;
        }

        [NotNull]
        private List<IQuadTreeNode> CreateQuadTreeNodesOnMultithreds(int segmentSize)
        {
            var processorCount = Environment.ProcessorCount;
            waitHandles = new WaitHandle[processorCount];
            var subsectors = 1;
            var subsectorSideSize = segmentSize;
            var subSectorsInRaw = 1;
            while (subsectors < processorCount)
            {
                subsectors *= 4;
                subSectorsInRaw *= 2;
                subsectorSideSize /= 2;
            }
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

                            allSubsectors[j] = new QuadTreeNodeMerger().Merge(allLeafs);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    are.Set();
                });
                ThreadPool.QueueUserWorkItem(waitCallback, waitHandles[i]);
            }
            WaitHandle.WaitAll(waitHandles);
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
            var planetDataProvider = new PlanetFactory(mConstants, new Random(), new PlanetComparer(mPlayer, mConstants));
            for (int i = 0; i < segmentsInSector; ++i)
            {
                float y = i / segmentsRaw;
                float x = i - y * segmentsRaw;
                x = segmentX - segmentHalfSize + x * mConstants.GetSectorSideSize() + sectorHalfSize;
                y = segmentY + segmentHalfSize - y * mConstants.GetSectorSideSize() - sectorHalfSize;
                
                var box = new AABBox(x, y, mConstants.GetSectorSideSize(), mConstants.GetSectorSideSize());
                sectorStore.Add(new QuadTreeLeaf(box, planetDataProvider.CreatePlanetsForSector(), mConstants, mPlayer));
            }
            return sectorStore;
        }
    }
}
