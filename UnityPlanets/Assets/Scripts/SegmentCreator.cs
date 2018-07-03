using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Planets;

namespace Assets.Scripts
{
    public class SegmentCreator : ISegmentCreator
    {
        private readonly IConstants mConstants;
        private readonly ISectorCreator mSectorCreator;
        private readonly int mSectorsInRaw;

        public SegmentCreator(IConstants constants, ISectorCreator sectorCreator)
        {
            mConstants = constants;
            mSectorCreator = sectorCreator;
            mSectorsInRaw = 100;
        }

        public ISector[] CreateSectors()
        {
            int negativeInd = -mSectorsInRaw / 2;
            int positiveInd = mSectorsInRaw / 2;

            var sectorStore = new ISector[mSectorsInRaw * mSectorsInRaw];
            if (positiveInd == negativeInd)
            {
                sectorStore[0] = mSectorCreator.CreateSector(-1, -1);
            }
            else
            {
                int i = 0;
                for (int y = negativeInd; y < positiveInd; ++y)
                {
                    for (int x = negativeInd; x < positiveInd; ++x)
                    {
                        sectorStore[i] = mSectorCreator.CreateSector(x, y);
                        ++i;
                    }

                }
            }
            return sectorStore;
        }
    }
}
