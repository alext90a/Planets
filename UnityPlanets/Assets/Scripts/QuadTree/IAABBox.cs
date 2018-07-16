using JetBrains.Annotations;

namespace QuadTree
{
    public interface IAABBox
    {
        float GetX();
        float GetY();
        float GetWidth();
        float GetHeight();
        bool IsIntersect([NotNull]IAABBox other);
    }
}
