using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Assets.Scripts.QuadTree
{
    public interface IQuadTreeNode
    {
        void GetVisiblePlanets([NotNull]IAABBox cameraBox, [NotNull]List<PlanetData> visiblePlanets);
        [NotNull]IAABBox GetAABBox();
        void VisitNodes(INodeVisitor nodeVisitor);
    }
}
