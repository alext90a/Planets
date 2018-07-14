using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Assets.Scripts.QuadTree
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
