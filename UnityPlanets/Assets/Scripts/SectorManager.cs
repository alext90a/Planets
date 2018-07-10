using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using JetBrains.Annotations;

namespace Planets
{
    public class SectorManager : ISectorManager
    {

        private readonly ISegmentCreator mSegmentCreator;
        public ISector[] mSectorStore;
        //const int totalSize = 10000;
        

        private ICamera mCamera;
        private readonly IConstants mConstants;
        private readonly IPlayer mPlayer;
        private readonly IVisiblePlanetsProvider mVisiblePlanetsProvider;

        public SectorManager(ISegmentCreator segmentCreator, IConstants constants, ICamera camera, IPlayer player, IVisiblePlanetsProvider planetsProvider)
        {
            mSegmentCreator = segmentCreator;
            mConstants = constants;
            mCamera = camera;
            mPlayer = player;
            mVisiblePlanetsProvider = planetsProvider;
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
            mVisiblePlanetsProvider.GetVisiblePlanets(planets, mSectorStore);
        }

        

        private PlanetData GetPlanetData(int sectorX, int sectorY, int planet)
        {
            int planetCoord = planet % mConstants.GetMaxPlanetScore();
            int planetX = planetCoord % 100 + sectorX * mConstants.GetSectorSideSize();
            int planetY = planetCoord / 100 + sectorY * mConstants.GetSectorSideSize();
            return new PlanetData(planetX, planetY, planet / mConstants.GetCellsInSector());
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
