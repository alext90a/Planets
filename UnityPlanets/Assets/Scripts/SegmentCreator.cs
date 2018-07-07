using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Planets;

namespace Assets.Scripts
{
    public class SegmentCreator : ISegmentCreator
    {
        private readonly IConstants mConstants;
        private readonly ISectorCreator mSectorCreator;
        private readonly int mSectorsInRaw;

        private static WaitHandle[] waitHandles;
        private ISector[] mSectorStore;

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
            int sectorsAmount = mSectorsInRaw * mSectorsInRaw;
            var mSectorStore = new ISector[sectorsAmount];


            //for (int i = 0; i < sectorsAmount; ++i)
            //{
            //    var y = i / mSectorsInRaw;
            //    var x = i - y * mSectorsInRaw;
            //    mSectorStore[i] = mSectorCreator.CreateSector(x, y);
            //}



            //var threads = new int[] { 0, 2500, 5000, 7500 };
            //Parallel.ForEach(threads, (curStart) =>
            //{
            //    for (int i = curStart; i < curStart + 2500; ++i)
            //    {
            //        var y = i / mSectorsInRaw;
            //        var x = i - y * mSectorsInRaw;
            //        var sector = new Sector(mConstants, new PlanetComparer(new Player(), mConstants), x, y);
            //        sector.Init();
            //        mSectorStore[i] = sector;// mSectorCreator.CreateSector(x, y);
            //    }
            //});


            var processorCount = Environment.ProcessorCount;
            waitHandles = new WaitHandle[processorCount];
            
            var itemsPerThread = sectorsAmount / processorCount;
            for (int k = 0; k < processorCount; ++k)
            {
                waitHandles[k] = new AutoResetEvent(false);
                var startIndex = k * itemsPerThread;
                var endIndex = startIndex + itemsPerThread;


                var waitCallback = new WaitCallback((Object state) =>
                {
                    AutoResetEvent are = (AutoResetEvent)state;
                    for (int i = startIndex; i < endIndex; ++i)
                    {
                        var y = i / mSectorsInRaw;
                        var x = i - y * mSectorsInRaw;
                        var sector = new Sector(mConstants, new PlanetComparer(new Player(), mConstants), x, y);
                        sector.Init();
                        mSectorStore[i] = sector;// mSectorCreator.CreateSector(x, y);
                    }
                    are.Set();
                });
                ThreadPool.QueueUserWorkItem(waitCallback, waitHandles[k]);
            }
            WaitHandle.WaitAll(waitHandles);

            //if (positiveInd == negativeInd)
            //{
            //    sectorStore[0] = mSectorCreator.CreateSector(-1, -1);
            //}
            //else
            //{
            //    int i = 0;
            //    for (int y = negativeInd; y < positiveInd; ++y)
            //    {
            //        for (int x = negativeInd; x < positiveInd; ++x)
            //        {
            //            sectorStore[i] = mSectorCreator.CreateSector(x, y);
            //            ++i;
            //        }

            //    }
            //}
            return mSectorStore;
        }

        private void ThreadSegmentCreator(int startIndex, int endIndex)
        {
            for (int i = 0; i < endIndex; ++i)
            {
                var y = i / mSectorsInRaw;
                var x = i - y * mSectorsInRaw;
                mSectorStore[i] = mSectorCreator.CreateSector(x, y);
            }
        }
    }
}
