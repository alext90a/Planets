﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;

namespace Planets
{
    public class SectorManager : ISectorManager
    {
        private readonly ISectorCreator mSectorCreator;
        

        public ISector[] mCellStore;
        //const int totalSize = 10000;
        

        private ICamera mCamera;
        private readonly IConstants mConstants;
        public static int mTotalBig = 0;
        public static List<int> mBigs = new List<int>(10000);

        public SectorManager(ISectorCreator sectorCreator, IConstants constants, ICamera camera)
        {
            mSectorCreator = sectorCreator;
            mConstants = constants;
            mCamera = camera;
            //mCellStore = new ISector[mConstants.SectorsInSegment];
            mCellStore = new ISector[16];
        }

        public void Init()
        {
            var watcher1 = new Stopwatch();
            watcher1.Start();
            int raws = mConstants.MaxCameraSize / mConstants.SectorSideSize;
            int negativeInd = -raws / 2;
            int positiveInd = raws / 2;
            positiveInd = 2;
            negativeInd = -2;
            if (positiveInd == negativeInd)
            {
                mCellStore[0] = mSectorCreator.CreateSector(-1, -1);
            }
            else
            {
                int i = 0;
                for (int y = negativeInd; y < positiveInd; ++y)
                {
                    for (int x = negativeInd; x < positiveInd; ++x)
                    {
                        mCellStore[i] = mSectorCreator.CreateSector(x, y);
                        ++i;
                    }

                }
            }
            
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

            var startSector = mCellStore[0];
            for (int i = 0; i < mConstants.PlanetsToVisualize; ++i)
            {
                if (!IsPlanetInCamera(mCamera, startSector.GetPlanet(i), startSector.GetX,
                    startSector.GetY))
                {
                    continue;
                }

                
                planets.Add(GetPlanetData(startSector.GetX, startSector.GetY, startSector.GetPlanet(i)));
                //sectors.Add(0);
            }

            for (int i = 1; i < mCellStore.Length; ++i)
            {
                var inspectedSector = mCellStore[i];
                int posToInsert = -1;

                for (int k = 0; k < mConstants.PlanetsInSector; ++k)
                {
                    if (!IsPlanetInCamera(mCamera, inspectedSector.GetPlanet(k), inspectedSector.GetX,
                        inspectedSector.GetY))
                    {
                        continue;
                    }

                    for (int j = mConstants.PlanetsToVisualize-1; j > -1; --j)
                    {

                        if (j >= planets.Count || j < 0)
                        {
                            int adfas = 0;
                            ++adfas;
                        }
                        if (planets[j].Score < inspectedSector.GetPlanetRating(k))
                        {
                            posToInsert = j;
                            continue;
                        }
                        break;
                    }
                    if (posToInsert != -1)
                    {
                        planets[posToInsert] = new PlanetData(inspectedSector.GetX, inspectedSector.GetY, inspectedSector.GetPlanetRating(k));
                        //sectors[posToInsert] = i;
                        posToInsert = -1;
                        continue;
                    }
                    break;
                }
            }
            mBigs.Sort(Sector.Compare);
        }

        private PlanetData GetPlanetData(int sectorX, int sectorY, int planet)
        {
            int planetCoord = planet % mConstants.MaxPlanetScore;
            int planetX = planetCoord % 100 + sectorX * mConstants.SectorSideSize;
            int planetY = planetCoord / 100 + sectorY * mConstants.SectorSideSize;
            return new PlanetData(planetX, planetY, planet / mConstants.CellsInSector);
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
