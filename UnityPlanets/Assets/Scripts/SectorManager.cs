using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;

namespace Planets
{
    public class SectorManager : ISectorManager
    {

        private readonly ISegmentCreator mSegmentCreator;
        public ISector[] mSectorStore;
        //const int totalSize = 10000;
        

        private ICamera mCamera;
        private readonly IConstants mConstants;

        public SectorManager(ISegmentCreator segmentCreator, IConstants constants, ICamera camera)
        {
            mSegmentCreator = segmentCreator;
            mConstants = constants;
            mCamera = camera;
        }

        public void Init()
        {
            var watcher1 = new Stopwatch();
            watcher1.Start();

            mSectorStore = mSegmentCreator.CreateSectors();
            watcher1.Stop();

            var watcher = new Stopwatch();
            watcher.Start();
            //FindRenderPlanets();
            watcher.Stop();
        }

        public void GetVisiblePlanets(List<PlanetData> planets)
        {
            planets.Clear();
            //sectors.Clear();

            var cameraTop = mCamera.GetTop();
            var cameraBottom = mCamera.GetBottom();
            var cameraLeft = mCamera.GetLeft();
            var cameraRight = mCamera.GetRight();

            var startSector = mSectorStore[0];
            for (int i = 0; i < mConstants.GetPlanetsToVisualize(); ++i)
            {
                if (!IsPlanetInCamera(startSector.GetPlanet(i), startSector.GetX(),
                    startSector.GetY(), cameraTop, cameraLeft, cameraBottom, cameraRight))
                {
                    continue;
                }

                
                planets.Add(GetPlanetData(startSector.GetX(), startSector.GetY(), startSector.GetPlanet(i)));
                //sectors.Add(0);
            }

            for (int i = 1; i < mSectorStore.Length; ++i)
            {
                var inspectedSector = mSectorStore[i];

                if (!IsCameraInercectSector(inspectedSector.GetX(), inspectedSector.GetY(), cameraTop, cameraBottom, cameraLeft, cameraRight))
                {
                    continue;
                }

                int posToInsert = -1;

                for (int k = 0; k < mConstants.GetPlanetsInSector(); ++k)
                {
                    if (!IsPlanetInCamera(inspectedSector.GetPlanet(k), inspectedSector.GetX(),
                        inspectedSector.GetY(), cameraTop, cameraLeft, cameraBottom, cameraRight))
                    {
                        continue;
                    }

                    for (int j = planets.Count-1; j > -1; --j)
                    {

                        if (planets[j].Score < inspectedSector.GetPlanetRating(k))
                        {
                            posToInsert = j;
                            continue;
                        }
                        break;
                    }
                    if (posToInsert != -1)
                    {
                        planets[posToInsert] = new PlanetData(inspectedSector.GetX(), inspectedSector.GetY(), inspectedSector.GetPlanetRating(k));
                        //sectors[posToInsert] = i;
                        posToInsert = -1;
                        continue;
                    }
                    break;
                }
            }
        }

        private PlanetData GetPlanetData(int sectorX, int sectorY, int planet)
        {
            int planetCoord = planet % mConstants.GetMaxPlanetScore();
            int planetX = planetCoord % 100 + sectorX * mConstants.GetSectorSideSize();
            int planetY = planetCoord / 100 + sectorY * mConstants.GetSectorSideSize();
            return new PlanetData(planetX, planetY, planet / mConstants.GetCellsInSector());
        }

        private bool IsPlanetInCamera(int planetData, int sectorX, int sectorY, int cameraTop, int cameraLeft, int cameraBottom, int cameraRight)
        {
            int planetCoord = planetData % 10000;
            int planetX = planetCoord % 100 + sectorX * 100;
            int planetY = planetCoord / 100 + sectorY * 100;

            if ((cameraTop >= planetY && cameraBottom <= planetY) &&
                (cameraLeft <= planetX && cameraRight >= planetX))
            {
                return true;
            }

            return false;
        }

        public bool IsCameraInercectSector(int sectorIndX, int sectorIndY, int cameraTop, int cameraBottom, int cameraLeft, int cameraRight)
        {
            int sectorLeft = sectorIndX * mConstants.GetSectorSideSize();
            int sectorRight = sectorLeft + mConstants.GetSectorSideSize() - 1;
            int sectorBottom = sectorIndY * mConstants.GetSectorSideSize();
            int sectorTop = sectorBottom + mConstants.GetSectorSideSize() - 1;

            
            float cameraX = cameraLeft + (cameraRight - cameraLeft) / 2f;
            float cameraY = cameraBottom + (cameraTop - cameraBottom) / 2f;
            float sectorX = sectorLeft + (sectorRight - sectorLeft) / 2f;
            float sectorY = sectorBottom + (sectorTop - sectorBottom) / 2f;

            float sectorWidth = mConstants.GetSectorSideSize();
            float sectorHeight = mConstants.GetSectorSideSize();
            float cameraWidth = cameraRight - cameraLeft + 1;
            float cameraHeight = cameraTop - cameraBottom + 1;

            return (Math.Abs(sectorX - cameraX) * 2 < (sectorWidth + cameraWidth)) &&
                   (Math.Abs(sectorY - cameraY) * 2 < (sectorHeight + cameraHeight));
        }

        private bool IsCameraInsideSector(ICamera camera, int sectorX, int sectorY)
        {
            int sectorLeft = sectorX * mConstants.GetSectorSideSize();
            int sectorRight = sectorLeft + mConstants.GetSectorSideSize();
            int sectorBottom = sectorY * mConstants.GetSectorSideSize();
            int sectorTop = sectorBottom + mConstants.GetSectorSideSize();

            //cameraInsideSector
            if ((camera.GetTop() <= sectorTop || camera.GetBottom() >= sectorBottom) &&
                (camera.GetLeft() >= sectorLeft && camera.GetRight() <= sectorRight))
            {
                return true;
            }
            return false;
        }

        private bool IsSectorInsideCamera(ICamera camera, int sectorX, int sectorY)
        {
            int sectorLeft = sectorX * mConstants.GetSectorSideSize();
            int sectorRight = sectorLeft + mConstants.GetSectorSideSize();
            int sectorBottom = sectorY * mConstants.GetSectorSideSize();
            int sectorTop = sectorBottom + mConstants.GetSectorSideSize();

            //cameraInsideSector
            if ((camera.GetTop() <= sectorTop || camera.GetBottom() >= sectorBottom) &&
                (camera.GetLeft() >= sectorLeft && camera.GetRight() <= sectorRight))
            {
                return true;
            }
            return false;
        }
        
    }
}
