using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;

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
    [NotNull]
    private readonly IAABBox mBox;

    public QuadTreeNode([NotNull]IAABBox box
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

    public void GetVisiblePlanets(IAABBox cameraBox, List<PlanetData> visiblePlanets)
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

    public IAABBox GetAABBox()
    {
        return mBox;
    }

    public void VisitNodes(INodeVisitor nodeVisitor)
    {
        mTopLeft.VisitNodes(nodeVisitor);
        mTopRight.VisitNodes(nodeVisitor);
        mBottomLeft.VisitNodes(nodeVisitor);
        mBottomRight.VisitNodes(nodeVisitor);
    }
}
