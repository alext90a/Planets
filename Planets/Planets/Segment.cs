using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets
{
    public class Segment
    {
        private ISectorCreator mSectorCreator;
        public Segment(ISectorCreator sectorCreator)
        {
            mSectorCreator = sectorCreator;

            var watcher1 = new Stopwatch();
            watcher1.Start();
            for (int i = 0; i < totalSize; ++i)
            {
                var y = i / 100;
                var x = i - y * 100;
                cellStore[i] = mSectorCreator.CreateSector(x, y);

                
            }
            watcher1.Stop();

            var watcher = new Stopwatch();
            watcher.Start();
            FindRenderPlanets();
            watcher.Stop();
        }

        private void FindRenderPlanets()
        {
            int[] best = new int[renderAmount];
            int[] bestSectors = new int[renderAmount];
            var startSector = cellStore[0];
            for (int i = 0; i < renderAmount; ++i)
            {
                best[i] = startSector.GetPlanet(i);
                bestSectors[i] = 0;
            }

            for (int i = 1; i < cellStore.Length; ++i)
            {
                var inspectedSector = cellStore[i];
                int posToInsert = -1;

                for (int k = 0; k < inspectedSector.PlanetsInSector; ++k)
                {
                    for (int j = renderAmount-1; j > -1; --j)
                    {
                        if (best[j] < inspectedSector.GetPlanet(k))
                        {
                            posToInsert = j;
                            continue;
                        }
                        break;
                    }
                    if (posToInsert != -1)
                    {
                        best[posToInsert] = inspectedSector.GetPlanet(k);
                        bestSectors[posToInsert] = i;
                        posToInsert = -1;
                        continue;
                    }
                    break;
                }
            }
            bigs.Sort(Sector.Compare);
        }
        public ISector[] cellStore = new ISector[totalSize];
        //const int totalSize = 10000;
        const int totalSize = 10000;

        private const int renderAmount = 10;

        public static int totalBig = 0;
        public static List<int> bigs = new List<int>(10000);
    }
}
