using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;
using QuadTree;

namespace Assets.Scripts
{
    public interface IStartUpNodeInitializer
    {
        void Run([NotNull] IAABBox startViewBox, [NotNull]IQuadTreeNode rootNode, [NotNull]IArrayBackgroundWorkerListener listener);
    }
}
