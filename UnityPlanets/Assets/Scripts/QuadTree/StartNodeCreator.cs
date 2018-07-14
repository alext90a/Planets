using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Planets;

namespace Assets.Scripts.QuadTree
{
    public sealed class StartNodeCreator
    {
        [NotNull] private readonly IConstants mConstants;
        [NotNull] private readonly IPlayer mPlayer;

        public StartNodeCreator([NotNull] IConstants constants, [NotNull] IPlayer player)
        {
            mConstants = constants;
            mPlayer = player;
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

            var planetDataProvider = new PlanetFactory(mConstants, new Random(), new PlanetComparer(mPlayer, mConstants));
            if (levels > 0)
            {
                var allLeafs = CreateStartLeafs(segmentSize);
                var currentLevel = levels;
                var startSize = (int)Math.Pow(2, levels);
                var mergedNodes = allLeafs;
                while (currentLevel != 0)
                {
                    mergedNodes = Merge(mergedNodes, startSize);
                    startSize /= 2;
                    --currentLevel;
                }
                rootNode = mergedNodes[0];
            }
            else
            {

                rootNode = new QuadTreeLeaf(new AABBox(0f, 0f, segmentSize, segmentSize),  planetDataProvider.CreatePlanetsForSector(), mConstants, mPlayer);
            }
            return rootNode;
        }

        [NotNull]
        private List<IQuadTreeNode> Merge([NotNull] List<IQuadTreeNode> mergedNodes, int startRawSize)
        {
            var endRawSize = startRawSize / 2;
            var resultSize = endRawSize * endRawSize;
            var resultList = new List<IQuadTreeNode>(resultSize);
            for (int i = 0; i < resultSize; ++i)
            {
                var index = i * 2 + i / endRawSize * startRawSize;
                var topLeft = mergedNodes[index];
                var topRight = mergedNodes[index + 1];
                var bottomLeft = mergedNodes[index + startRawSize];
                var bottomRight = mergedNodes[index + startRawSize + 1];
                var topLeftBox = topLeft.GetAABBox();
                var box = new AABBox(topLeftBox.GetX() + topLeftBox.GetWidth()/2f
                    , topLeftBox.GetY() - topLeftBox.GetHeight()/2f
                    , topLeftBox.GetWidth()*2f
                    , topLeftBox.GetHeight()*2f);
                resultList.Add(new QuadTreeNode(box, topLeft, topRight, bottomLeft, bottomRight));
            }
            return resultList;
        }

        [NotNull]
        private List<IQuadTreeNode> CreateStartLeafs(int segmentSize)
        {
            var segmentsRaw = segmentSize / mConstants.GetSectorSideSize();
            var segmentsInSector = segmentsRaw * segmentsRaw;
            var sectorStore = new List<IQuadTreeNode>(segmentsInSector);
            var segmentHalfSize = segmentSize / 2f;
            var sectorHalfSize = mConstants.GetSectorSideSize() / 2f;
            var planetDataProvider = new PlanetFactory(mConstants, new Random(), new PlanetComparer(mPlayer, mConstants));
            for (int i = 0; i < segmentsInSector; ++i)
            {
                float y = i / segmentsRaw;
                float x = i - y * segmentsRaw;
                x = -segmentHalfSize + x * mConstants.GetSectorSideSize() + sectorHalfSize;
                y = segmentHalfSize - y * mConstants.GetSectorSideSize() - sectorHalfSize;
                var box = new AABBox(x, y, mConstants.GetSectorSideSize(), mConstants.GetSectorSideSize());
                sectorStore.Add(new QuadTreeLeaf(box, planetDataProvider.CreatePlanetsForSector(), mConstants, mPlayer));
            }
            return sectorStore;
        }
    }
}
