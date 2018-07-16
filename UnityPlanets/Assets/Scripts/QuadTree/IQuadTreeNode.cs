using JetBrains.Annotations;

namespace QuadTree
{
    public interface IQuadTreeNode
    {
        [NotNull]IAABBox GetAABBox();
        void VisitNodes([NotNull]INodeVisitor nodeVisitor);
        void VisitVisibleNodes([NotNull] IAABBox cameraBox, [NotNull] INodeVisitor nodeVisitor);
    }
}
