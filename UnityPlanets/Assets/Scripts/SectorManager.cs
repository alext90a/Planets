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

            var startSector = mSectorStore[0];
            for (int i = 0; i < mConstants.GetPlanetsToVisualize(); ++i)
            {
                if (!IsPlanetInCamera(mCamera, startSector.GetPlanet(i), startSector.GetX(),
                    startSector.GetY()))
                {
                    continue;
                }

                
                planets.Add(GetPlanetData(startSector.GetX(), startSector.GetY(), startSector.GetPlanet(i)));
                //sectors.Add(0);
            }

            for (int i = 1; i < mSectorStore.Length; ++i)
            {
                var inspectedSector = mSectorStore[i];

                if (!IsCameraInercectSector(mCamera, inspectedSector.GetX(), inspectedSector.GetY()))
                {
                    continue;
                }

                int posToInsert = -1;

                for (int k = 0; k < mConstants.GetPlanetsInSector(); ++k)
                {
                    if (!IsPlanetInCamera(mCamera, inspectedSector.GetPlanet(k), inspectedSector.GetX(),
                        inspectedSector.GetY()))
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

        private bool IsPlanetInCamera(ICamera camera, int planetData, int sectorX, int sectorY)
        {
            int planetCoord = planetData % 10000;
            int planetX = planetCoord % 100 + sectorX * 100;
            int planetY = planetCoord / 100 + sectorY * 100;

            if ((camera.GetTop() >= planetY && camera.GetBottom() <= planetY) &&
                (camera.GetLeft() <= planetX && camera.GetRight() >= planetX))
            {
                return true;
            }

            return false;
        }

        public bool IsCameraInercectSector(ICamera camera, int sectorIndX, int sectorIndY)
        {
            int sectorLeft = sectorIndX * mConstants.GetSectorSideSize();
            int sectorRight = sectorLeft + mConstants.GetSectorSideSize() - 1;
            int sectorBottom = sectorIndY * mConstants.GetSectorSideSize();
            int sectorTop = sectorBottom + mConstants.GetSectorSideSize() - 1;

            
            float cameraX = camera.GetLeft() + (camera.GetRight() - camera.GetLeft()) / 2f;
            float cameraY = camera.GetBottom() + (camera.GetTop() - camera.GetBottom()) / 2f;
            float sectorX = sectorLeft + (sectorRight - sectorLeft) / 2f;
            float sectorY = sectorBottom + (sectorTop - sectorBottom) / 2f;

            float sectorWidth = mConstants.GetSectorSideSize();
            float sectorHeight = mConstants.GetSectorSideSize();
            float cameraWidth = camera.GetRight() - camera.GetLeft() + 1;
            float cameraHeight = camera.GetTop() - camera.GetBottom() + 1;

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
