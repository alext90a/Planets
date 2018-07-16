using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using QuadTree;

namespace Assets.Scripts
{
    public class VisibleNodesCollector : INodeVisitor
    {
        [NotNull]
        private List<QuadTreeLeaf> mVisibleLeaves = new List<QuadTreeLeaf>();
        public void AddVisited(QuadTreeLeaf visitedLeaf)
        {
            mVisibleLeaves.Add(visitedLeaf);
        }

        [NotNull]
        public IReadOnlyList<QuadTreeLeaf> GetVisibleLeaves()
        {
            return mVisibleLeaves;
        }
    }
}
