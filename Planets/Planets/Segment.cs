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
        private readonly ISectorCreator mSectorCreator;
        

        public ISector[] cellStore;
        //const int totalSize = 10000;

        private const int renderAmount = 10;

        private ICamera mCamera = new Camera();
        private readonly IConstants mConstants;
        public static int totalBig = 0;
        public static List<int> bigs = new List<int>(10000);

        public Segment(ISectorCreator sectorCreator, IConstants constants)
        {
            mSectorCreator = sectorCreator;
            mConstants = constants;
            cellStore = new ISector[mConstants.SectorsInSegment];
            
        }

        public void Init()
        {
            var watcher1 = new Stopwatch();
            watcher1.Start();
            for (int i = 0; i < mConstants.SectorsInSegment; ++i)
            {
                var y = i / mConstants.SectorSideSize;
                var x = i - y * mConstants.SectorSideSize;
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
                if (!IsPlanetInCamera(mCamera, startSector.GetPlanet(i), startSector.GetX,
                    startSector.GetY))
                {
                    continue;
                }

                best[i] = startSector.GetPlanet(i);
                bestSectors[i] = 0;
            }

            for (int i = 1; i < cellStore.Length; ++i)
            {
                var inspectedSector = cellStore[i];
                int posToInsert = -1;

                for (int k = 0; k < mConstants.PlanetsInSector; ++k)
                {
                    if (!IsPlanetInCamera(mCamera, inspectedSector.GetPlanet(k), inspectedSector.GetX,
                        inspectedSector.GetY))
                    {
                        continue;
                    }

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

        private bool IsPlanetInCamera(ICamera camera, int planetData, int sectorX, int sectorY)
        {
            int planetCoord = planetData % mConstants.MaxPlanetScore;
            int planetX = planetCoord % 100 + sectorX * mConstants.SectorSideSize;
            int planetY = planetCoord / 100 + sectorY * mConstants.SectorSideSize;

            if ((camera.Top >= planetY && camera.Bottom <= planetY) &&
                (camera.Left <= planetX && camera.Right >= planetX))
            {
                return true;
            }

            return false;
        }

        public bool IsCameraInercectSector(ICamera camera, int sectorIndX, int sectorIndY)
        {
            int sectorLeft = sectorIndX * mConstants.SectorSideSize;
            int sectorRight = sectorLeft + mConstants.SectorSideSize - 1;
            int sectorBottom = sectorIndY * mConstants.SectorSideSize;
            int sectorTop = sectorBottom + mConstants.SectorSideSize - 1;

            
            float cameraX = camera.Left + (camera.Right - camera.Left) / 2f;
            float cameraY = camera.Bottom + (camera.Top - camera.Bottom) / 2f;
            float sectorX = sectorLeft + (sectorRight - sectorLeft) / 2f;
            float sectorY = sectorBottom + (sectorTop - sectorBottom) / 2f;

            float sectorWidth = mConstants.SectorSideSize;
            float sectorHeight = mConstants.SectorSideSize;
            float cameraWidth = camera.Right - camera.Left + 1;
            float cameraHeight = camera.Top - camera.Bottom + 1;

            return (Math.Abs(sectorX - cameraX) * 2 < (sectorWidth + cameraWidth)) &&
                   (Math.Abs(sectorY - cameraY) * 2 < (sectorHeight + cameraHeight));
        }

        private bool IsCameraInsideSector(ICamera camera, int sectorX, int sectorY)
        {
            int sectorLeft = sectorX * mConstants.SectorSideSize;
            int sectorRight = sectorLeft + mConstants.SectorSideSize;
            int sectorBottom = sectorY * mConstants.SectorSideSize;
            int sectorTop = sectorBottom + mConstants.SectorSideSize;

            //cameraInsideSector
            if ((camera.Top <= sectorTop || camera.Bottom >= sectorBottom) &&
                (camera.Left >= sectorLeft && camera.Right <= sectorRight))
            {
                return true;
            }
            return false;
        }

        private bool IsSectorInsideCamera(ICamera camera, int sectorX, int sectorY)
        {
            int sectorLeft = sectorX * mConstants.SectorSideSize;
            int sectorRight = sectorLeft + mConstants.SectorSideSize;
            int sectorBottom = sectorY * mConstants.SectorSideSize;
            int sectorTop = sectorBottom + mConstants.SectorSideSize;

            //cameraInsideSector
            if ((camera.Top <= sectorTop || camera.Bottom >= sectorBottom) &&
                (camera.Left >= sectorLeft && camera.Right <= sectorRight))
            {
                return true;
            }
            return false;
        }
        
    }
}
