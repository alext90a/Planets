﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;
using Planets;
using UnityEngine;

public class QuadTreeNode : IQuadTreeNode
{
    [NotNull]
    private IQuadTreeNode mTopLeft;
    [NotNull]
    private IQuadTreeNode mTopRight;
    [NotNull]
    private IQuadTreeNode mBottomLeft;
    [NotNull]
    private IQuadTreeNode mBottomRight;

    private readonly AABBox mBox;

    public QuadTreeNode(AABBox box
        ,[NotNull] IQuadTreeNode topLeft
        , [NotNull] IQuadTreeNode topRight
        , [NotNull] IQuadTreeNode bottomLeft
        , [NotNull] IQuadTreeNode bottomRight)
    {
        mBox = box;
        mTopLeft = topLeft;
        mTopRight = topRight;
        mBottomLeft = bottomLeft;
        mBottomRight = bottomRight;
    }

    public void GetVisiblePlanets(AABBox cameraBox, List<PlanetData> visiblePlanets)
    {
        if (!mBox.IsIntersect(cameraBox))
        {
            return;
        }
        mTopLeft.GetVisiblePlanets(cameraBox, visiblePlanets);
        mTopRight.GetVisiblePlanets(cameraBox, visiblePlanets);
        mBottomLeft.GetVisiblePlanets(cameraBox, visiblePlanets);
        mBottomRight.GetVisiblePlanets(cameraBox, visiblePlanets);
    }

    public AABBox GetAABBox()
    {
        return mBox;
    }
}
