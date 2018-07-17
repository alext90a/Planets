using System;
using System.Threading;
using JetBrains.Annotations;

namespace QuadTree
{
    public interface IThreadQuadTreeNodeCreator
    {
        [NotNull]
        WaitCallback GetWaitCallback();

        [CanBeNull]
        Exception GetException();

        [NotNull]
        WaitHandle GetWaitHandle();
    }
}
