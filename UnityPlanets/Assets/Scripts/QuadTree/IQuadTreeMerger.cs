using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using QuadTree;

namespace Assets.Scripts.QuadTree
{
    public interface IQuadTreeNodeMerger
    {
        [NotNull]
        IQuadTreeNode Merge([NotNull] List<IQuadTreeNode> mergedNodes);
    }
}
