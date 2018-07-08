using System;
using Assets.Scripts;
using JetBrains.Annotations;
using Planets;

namespace Editor.UnitTests
{
    public sealed class SegmentCreatorMock : ISegmentCreator
    {
        public ISector[] CreateSectors()
        {
            return mCreatFunc();
        }

        [NotNull]
        private Func<ISector[]> mCreatFunc = () => new ISector[0];

        public void SetupCreateSectors([NotNull]Func<ISector[]> sectorCreator)
        {
            mCreatFunc = sectorCreator;
        }
        
        
    }
}
