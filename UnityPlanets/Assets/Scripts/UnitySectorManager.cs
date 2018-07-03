using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Planets;
using UnityEngine;
using Zenject;

public class UnitySectorManager : MonoBehaviour, IBordersChangeListener
{

    [Inject] private readonly ISectorManager mSectorManager;
    [Inject] private readonly ICamera mCamera;
    [Inject] private readonly IConstants mConstants;
    [Inject] private readonly IUnityPlanetVisualizer mPlanetVisualizer;


    private List<PlanetData> mPlanets;

    void Awake()
    {
        mPlanets = new List<PlanetData>(mConstants.GetPlanetsToVisualize());
    }

	// Use this for initialization
	void Start () {

        mCamera.AddBorderChangeListener(this);
	    mSectorManager.Init();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void NewBorders(int top, int bottom, int left, int right)
    {
        mSectorManager.GetVisiblePlanets(mPlanets);
        mPlanetVisualizer.Visualize(mPlanets);
    }
}
