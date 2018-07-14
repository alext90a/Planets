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
        void GetVisiblePlanets(AABBox cameraBox, [NotNull]List<PlanetData> visiblePlanets);
        AABBox GetAABBox();
    }
}
