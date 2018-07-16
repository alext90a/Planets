using Assets.Scripts;
using JetBrains.Annotations;
using Planets;

namespace QuadTree
{
    public sealed class QuadTreeLeaf : IQuadTreeNode
    {
        [NotNull]
        private readonly IAABBox mBox;
        [CanBeNull]
        private int[] mPlanetRawData;
        [NotNull]
        private readonly IConstants mConstants;

        public QuadTreeLeaf([NotNull]IAABBox box
            , [NotNull] IConstants constants)
        {
            mBox = box;
            mConstants = constants;
        }

        

        public IAABBox GetAABBox()
        {
            return mBox;
        }

        public void SetPlanets([NotNull] int[] planetData)
        {
            mPlanetRawData = planetData;
        }

        public void VisitNodes(INodeVisitor nodeVisitor)
        {
            nodeVisitor.AddVisited(this);
        }

        public bool IsPlanetDataNull()
        {
            if (mPlanetRawData != null)
            {
                return false;
            }
            return true;
        }

        public void VisitVisibleNodes(IAABBox cameraBox, INodeVisitor nodeVisitor)
        {
            if (!mBox.IsIntersect(cameraBox))
            {
                return;
            }
            VisitNodes(nodeVisitor);
        }

        public int[] GetPlanetsRawData()
        {
            return mPlanetRawData;
        }


        public PlanetData GetPlanetData(int index)
        {
            // ReSharper disable once PossibleNullReferenceException
            var data = mPlanetRawData[index];
            var score = data / mConstants.GetCellsInSector();
            var posLocal = data - score * mConstants.GetMaxPlanetScore();
            var posLocY = posLocal / mConstants.GetSectorSideSize();
            var posLocX = posLocal - posLocY * mConstants.GetSectorSideSize();
            return new PlanetData(mBox.GetX() - mBox.GetWidth()/2 + posLocX, mBox.GetY() + mBox.GetHeight()/2 - posLocY, score);
        }

        public int GetPlanetRating(int index)
        {
            // ReSharper disable once PossibleNullReferenceException
            return mPlanetRawData[index] / mConstants.GetCellsInSector();
        }
    }
}
