using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using QuadTree;

namespace Assets.Scripts.QuadTree
{
    public class QuadTreeNodeMerger : IQuadTreeNodeMerger
    {
        public IQuadTreeNode Merge(List<IQuadTreeNode> mergedNodes)
        {
            while (mergedNodes.Count != 1)
            {
                mergedNodes = MergeInternal(mergedNodes);
            }
            return mergedNodes[0];
        }

        [NotNull]
        private List<IQuadTreeNode> MergeInternal([NotNull]List<IQuadTreeNode> mergedNodes)
        {
            int startRawSize = 1;
            do
            {
                startRawSize *= 2;
            } while (startRawSize * startRawSize < mergedNodes.Count);

            var endRawSize = startRawSize / 2;
            var resultSize = endRawSize * endRawSize;
            var resultList = new List<IQuadTreeNode>(resultSize);
            for (int i = 0; i < resultSize; ++i)
            {
                var index = i * 2 + i / endRawSize * startRawSize;
                var topLeft = mergedNodes[index];
                var topRight = mergedNodes[index + 1];
                var bottomLeft = mergedNodes[index + startRawSize];
                var bottomRight = mergedNodes[index + startRawSize + 1];
                // ReSharper disable once PossibleNullReferenceException
                var topLeftBox = topLeft.GetAABBox();
                var box = new AABBox(topLeftBox.GetX() + topLeftBox.GetWidth() / 2f
                    , topLeftBox.GetY() - topLeftBox.GetHeight() / 2f
                    , topLeftBox.GetWidth() * 2f
                    , topLeftBox.GetHeight() * 2f);
                resultList.Add(new QuadTreeNode(box, topLeft, topRight, bottomLeft, bottomRight));
            }
            return resultList;
        }

        
    }
}
