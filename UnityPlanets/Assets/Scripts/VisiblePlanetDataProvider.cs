using System;
using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using QuadTree;

public sealed class VisiblePlanetDataProvider : IVisiblePlanetDataProvider
{
    [NotNull] private readonly INodeVisitor mNodeVisitor;
    [NotNull] private readonly IRootNodeProvider mRootNodeProvider;
    [NotNull] private readonly IPlayer mPlayer;
    [NotNull] private readonly IConstants mConstants;
    [NotNull] private readonly List<PlanetData> mVisiblePlanets;

    public VisiblePlanetDataProvider([NotNull] INodeVisitor nodeVisitor
        , [NotNull] IRootNodeProvider rootNodeProvider
        , [NotNull] IPlayer player
        , [NotNull] IConstants constants)
    {
        mNodeVisitor = nodeVisitor;
        mRootNodeProvider = rootNodeProvider;
        mPlayer = player;
        mConstants = constants;
        mVisiblePlanets = new List<PlanetData>(mConstants.GetPlanetsToVisualize());
    }
            
    public IReadOnlyList<PlanetData> GetVisiblePlanets(IAABBox viewBBox)
    {
        mVisiblePlanets.Clear();
        mNodeVisitor.Clear();
        mRootNodeProvider.GetRootNote().VisitVisibleNodes(viewBBox, mNodeVisitor);
        var leafCollection = mNodeVisitor.GetVisibleLeaves();
        for (int i = 0; i < leafCollection.Count; ++i)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            InspectLeaf(leafCollection[i], viewBBox);
        }
        return mVisiblePlanets;
    }

    private void InspectLeaf([NotNull]QuadTreeLeaf visitedLeaf, [NotNull]IAABBox viewBox)
    {
        var planetRawData = visitedLeaf.GetPlanetsRawData();
        if (planetRawData == null)
        {
            return;
        }
        for (int i = 0; i < planetRawData.Length; ++i)
        {
            if (!IsPlanetInCamera(i, viewBox, visitedLeaf))
            {
                continue;
            }

            var posToInsert = FindPosToInsert(mVisiblePlanets, i, visitedLeaf);

            if (posToInsert != -1)
            {
                mVisiblePlanets.Insert(posToInsert, visitedLeaf.GetPlanetData(i));
                if (mVisiblePlanets.Count > mConstants.GetPlanetsToVisualize())
                {
                    mVisiblePlanets.RemoveAt(mVisiblePlanets.Count - 1);
                }
                continue;
            }
            if (mVisiblePlanets.Count < mConstants.GetPlanetsToVisualize())
            {
                mVisiblePlanets.Add(visitedLeaf.GetPlanetData(i));
                continue;
            }
            break;
        }
    }

    private bool IsPlanetInCamera(int planetIndex, [NotNull]IAABBox cameraBox, [NotNull]QuadTreeLeaf visitedLeaf)
    {
        var planetData = visitedLeaf.GetPlanetData(planetIndex);
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

    private int FindPosToInsert([NotNull]List<PlanetData> planets, int planetIndex, [NotNull]QuadTreeLeaf visitedLeaf)
    {
        var posToInsert = -1;
        for (int j = mConstants.GetPlanetsToVisualize() - 1; j > -1; --j)
        {

            if (j > planets.Count - 1)
            {
                continue;
            }

            var inStoreDistance = Math.Abs(mPlayer.Score - planets[j].Score);
            var pretenderDistance = Math.Abs(mPlayer.Score - visitedLeaf.GetPlanetRating(planetIndex));
            if (inStoreDistance > pretenderDistance)
            {
                posToInsert = j;
                continue;
            }
            if (inStoreDistance == pretenderDistance)
            {
                var pretenderPlanet = visitedLeaf.GetPlanetData(planetIndex);
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
}