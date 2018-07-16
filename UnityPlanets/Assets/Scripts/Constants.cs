using Planets;
using UnityEngine;

public class Constants : MonoBehaviour, IConstants
{
    [SerializeField]
    private int mMinCameraSize = 5;
    [SerializeField]
    private int mMaxCameraSize = 10000;
    [SerializeField]
    private int mSectorSideSize = 100;
    [SerializeField][Range(0f, 1f)]
    private float mPlanetsPercent = 0.3f;
    [SerializeField]
    private int mMaxPlanetScore = 10000;
    [SerializeField]
    private int mMinPlanetScore = 0;
    [SerializeField]
    private int mPlanetsToVisualize = 20;
    [SerializeField]
    private int mPlayerScore = 5000;

    private readonly int mCellsInSector;
    private readonly int mSectorsInSegment;
    private readonly int mPlanetsInSector;
        

    public Constants()
    {
        if (mPlayerScore > mMaxPlanetScore)
        {
            mPlayerScore = mMaxPlanetScore;
        }
        if (mPlayerScore < mMinPlanetScore)
        {
            mPlayerScore = mMinPlanetScore;
        }
        mCellsInSector = mSectorSideSize * mSectorSideSize;
        mSectorsInSegment = (mMaxCameraSize * mMaxCameraSize) / (mCellsInSector);
        mPlanetsInSector = (int)(mCellsInSector * mPlanetsPercent);
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
    public int GetPlayerScore()
    {
        return mPlayerScore;
    }
}