using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;
using QuadTree;

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

    public void VisitVisibleNodes(IAABBox cameraBox, INodeVisitor nodeVisitor)
    {
        if (!mBox.IsIntersect(cameraBox))
        {
            return;
        }
        mTopLeft.VisitVisibleNodes(cameraBox, nodeVisitor);
        mTopRight.VisitVisibleNodes(cameraBox, nodeVisitor);
        mBottomLeft.VisitVisibleNodes(cameraBox, nodeVisitor);
        mBottomRight.VisitVisibleNodes(cameraBox, nodeVisitor);
    }
}
