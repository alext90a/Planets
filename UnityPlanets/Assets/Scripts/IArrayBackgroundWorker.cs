using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;

namespace Assets.Scripts
{
    public interface IArrayBackgroundWorker
    {
        void AddListener([NotNull] IArrayBackgroundWorkerListener listener);
        void Run([NotNull]IReadOnlyList<QuadTreeLeaf> mLoadingNodes, [NotNull]IPlanetFactoryCreator planetFactoryCreator);
    }
}
