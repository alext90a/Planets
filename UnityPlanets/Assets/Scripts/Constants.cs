﻿namespace Planets
{
    public class Constants : IConstants
    {
        private int mMinCameraSize;
        private int mMaxCameraSize;
        private int mSectorSideSize;
        private int mCellsInSector;
        private int mSectorsInSegment;
        private float mPlanetsPercent;
        private int mPlanetsInSector;
        private int mMaxPlanetScore;
        private int mMinPlanetScore;
        private int mPlanetsToVisualize;

        public Constants()
        {
            mMinCameraSize = 5;
            mMaxCameraSize = 1000;
            mSectorSideSize = 100;
            mCellsInSector = mSectorSideSize * mSectorSideSize;
            mSectorsInSegment = (mMaxCameraSize * mMaxCameraSize) / (mCellsInSector);
            mPlanetsPercent = 0.3f;
            mPlanetsInSector = (int)(mCellsInSector * mPlanetsPercent);
            mMaxPlanetScore = 10000;
            mMinPlanetScore = 0;
            mPlanetsToVisualize = 20;
        }
        public int SectorsInSegment { get { return mSectorsInSegment; } }
        public int PlanetsInSector { get { return mPlanetsInSector; } }
        public int CellsInSector { get { return mCellsInSector; } }
        public int SectorSideSize { get { return mSectorSideSize; } }
        public float PlanetsPercent { get { return mPlanetsPercent; } }
        public int MaxCameraSize { get { return mMaxCameraSize; } }
        public int MinCameraSize { get { return mMinCameraSize; } }
        public int MaxPlanetScore { get { return mMaxPlanetScore; } }
        public int MinPlanetScore { get { return mMinPlanetScore; }}
        public int PlanetsToVisualize { get { return mPlanetsToVisualize; } }
    }
}
