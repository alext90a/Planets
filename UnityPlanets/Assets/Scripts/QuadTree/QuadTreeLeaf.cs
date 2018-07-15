using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Planets;

namespace Assets.Scripts.QuadTree
{
    public sealed class QuadTreeLeaf : IQuadTreeNode
    {
        [NotNull]
        private readonly IAABBox mBox;
        [CanBeNull]
        private int[] mPlanetRawData;

        [NotNull] private readonly IConstants mConstants;
        [NotNull] private readonly IPlayer mPlayer;

        public QuadTreeLeaf([NotNull]IAABBox box
            , [NotNull] IConstants constants
            , [NotNull] IPlayer player)
        {
            mBox = box;
            mConstants = constants;
            mPlayer = player;
        }

        public void GetVisiblePlanets(IAABBox cameraBox, List<PlanetData> visiblePlanets)
        {
            if (!mBox.IsIntersect(cameraBox))
            {
                return;
            }
            if (mPlanetRawData == null)
            {
                return;
            }
            int posToInsert = -1;
            for (int i = 0; i < mPlanetRawData.Length; ++i)
            {
                if (!IsPlanetInCamera(i, cameraBox))
                {
                    continue;
                }

                posToInsert = FindPosToInsert(visiblePlanets, i);

                if (posToInsert != -1)
                {
                    visiblePlanets.Insert(posToInsert, GetPlanetData(i));
                    posToInsert = -1;
                    if (visiblePlanets.Count > mConstants.GetPlanetsToVisualize())
                    {
                        visiblePlanets.RemoveAt(visiblePlanets.Count - 1);
                    }
                    continue;
                }
                if (visiblePlanets.Count < mConstants.GetPlanetsToVisualize())
                {
                    visiblePlanets.Add(GetPlanetData(i));
                    continue;
                }
                break;
            }
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


        private bool IsPlanetInCamera(int planetIndex, [NotNull]IAABBox cameraBox)
        {
            var planetData = GetPlanetData(planetIndex);
            var cameraTop = cameraBox.GetY() + cameraBox.GetHeight() / 2f;
            var cameraBottom = cameraTop - cameraBox.GetHeight();
            var cameraLeft = cameraBox.GetX() - cameraBox.GetWidth() / 2f;
            var cameraRight = cameraLeft + cameraBox.GetWidth();

            if ((cameraTop >= planetData.Y && cameraBottom <= planetData.Y) &&
                (cameraLeft <= planetData.X && cameraRight >= planetData.X))
            {
                return true;
            }

            return false;
        }

        private int FindPosToInsert([NotNull]List<PlanetData> planets, int planetIndex)
        {
            var posToInsert = -1;
            for (int j = mConstants.GetPlanetsToVisualize() - 1; j > -1; --j)
            {

                if (j > planets.Count - 1)
                {
                    continue;
                }

                var inStoreDistance = Math.Abs(mPlayer.Score - planets[j].Score);
                var pretenderDistance = Math.Abs(mPlayer.Score - GetPlanetRating(planetIndex));
                if (inStoreDistance > pretenderDistance)
                {
                    posToInsert = j;
                    continue;
                }
                if (inStoreDistance == pretenderDistance)
                {
                    var pretenderPlanet = GetPlanetData(planetIndex);
                    var distanceToStore = (mPlayer.GetX() - planets[j].X) * (mPlayer.GetX() - planets[j].X)
                                          + (mPlayer.GetY() - planets[j].Y) * (mPlayer.GetY() - planets[j].Y);
                    var distanceToPretender = (mPlayer.GetX() - pretenderPlanet.X) * (mPlayer.GetX() - pretenderPlanet.X)
                                              + (mPlayer.GetY() - pretenderPlanet.Y) * (mPlayer.GetY() - pretenderPlanet.Y);
                    if (distanceToPretender < distanceToStore)
                    {
                        posToInsert = j;
                        continue;
                    }
                }
                break;
            }
            return posToInsert;
        }

        private PlanetData GetPlanetData(int index)
        {
            var data = mPlanetRawData[index];
            var score = data / mConstants.GetCellsInSector();
            var posLocal = data - score * mConstants.GetMaxPlanetScore();
            var posLocY = posLocal / mConstants.GetSectorSideSize();
            var posLocX = posLocal - posLocY * mConstants.GetSectorSideSize();
            return new PlanetData(mBox.GetX() - mBox.GetWidth()/2 + posLocX, mBox.GetY() + mBox.GetHeight()/2 - posLocY, score);
        }

        private int GetPlanetRating(int index)
        {
            return mPlanetRawData[index] / mConstants.GetCellsInSector();
        }
    }
}
