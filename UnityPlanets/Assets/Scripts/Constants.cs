namespace Planets
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
        public int GetSectorsInSegment() { return mSectorsInSegment; } 
        public int GetPlanetsInSector() { return mPlanetsInSector; } 
        public int GetCellsInSector() { return mCellsInSector; } 
        public int GetSectorSideSize() { return mSectorSideSize; } 
        public float GetPlanetsPercent() { return mPlanetsPercent; } 
        public int GetMaxCameraSize(){ return mMaxCameraSize; } 
        public int GetMinCameraSize(){ return mMinCameraSize; } 
        public int GetMaxPlanetScore(){ return mMaxPlanetScore; } 
        public int GetMinPlanetScore() { return mMinPlanetScore; }
        public int GetPlanetsToVisualize(){ return mPlanetsToVisualize; } 
    }
}
