using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using Boo.Lang.Runtime.DynamicDispatching;
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

        [NotNull] private IQuadTreeNode[] mLoadingNodes;

        public int mProgress = 0;
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
                rootNode = new QuadTreeLeaf(new AABBox(0f, 0f, segmentSize, segmentSize), mConstants, mPlayer);
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
                            var startIndex1 = startIndex * allLeafs.Count;
                            var endIndex1 = startIndex1 + allLeafs.Count;
                            var l = 0;
                            for (int k = startIndex1; k < endIndex1; ++k)
                            {
                                if (mLoadingNodes.Length <= k)
                                {
                                    int adfa;
                                    adfa = 9;
                                }
                                mLoadingNodes[k] = allLeafs[l];
                                ++l;
                            }

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

            BackgroundWorker worker = new BackgroundWorker();
            var progressMy = 0;
            var planetFactory = mPlanetFactoryCreator.CreatePlanetFactory();
            worker.DoWork += delegate(object sender, DoWorkEventArgs args)
            {
                try
                {
                    var progress = 0;
                    for (int i = 0; i < mLoadingNodes.Length; ++i)
                    {
                        if (mLoadingNodes[i] == null)
                        {
                            continue;
                        }
                        mLoadingNodes[i].VisitNodes(planetFactory);
                        var floatData = ((float) i / mLoadingNodes.Length) *100f;

                        worker.ReportProgress((int)floatData);
                    }
                }
                catch (Exception e)
                {
                    
                }
                
            };
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (sender, args) => { CallBackFromLoad(sender, args);};
            worker.RunWorkerAsync();
            return allSubsectorsList;
        }

        private void CallBackFromLoad(object sender, ProgressChangedEventArgs args)
        {
            mProgress = args.ProgressPercentage;
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
                sectorStore.Add(new QuadTreeLeaf(box, mConstants, mPlayer));
            }
            return sectorStore;
        }
    }
}
