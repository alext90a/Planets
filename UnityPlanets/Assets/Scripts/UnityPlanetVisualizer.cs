using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Planets;
using UnityEngine;
using Zenject;

public class UnityPlanetVisualizer : MonoBehaviour, IUnityPlanetVisualizer
{

    [Inject] private readonly IConstants mConstants;
    [Inject] private readonly ICamera mCamera;
    [Inject] private readonly UnityPlanetDataFactory mPlanetDataFactory;


    private List<IUnityPlanetData> mPlanets = new List<IUnityPlanetData>();
    
    private void Awake()
    {
        var child = transform.GetChild(0).gameObject;
        //child.SetActive(false);
        //mPlanets.Add(child);
        for (int i = 0; i < mConstants.GetPlanetsToVisualize(); ++i)
        {
            var planet = mPlanetDataFactory.Create().gameObject;
            planet.transform.parent = transform;
            planet.SetActive(false);
            mPlanets.Add(planet.GetComponent<IUnityPlanetData>());

        }
        child.SetActive(false);
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Visualize(List<PlanetData> planets)
    {
        for (int i = 0; i < mPlanets.Count; ++i)
        {
            mPlanets[i].Deactivate();
        }

        for(int i= 0; i < planets.Count; ++i)
        {
            mPlanets[i].Activate(planets[i].X, planets[i].Y, planets[i].Score);
        }
    }
}
