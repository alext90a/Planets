﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Planets;
using UnityEngine;
using Zenject;

public class UnityPlanetVisualizer : MonoBehaviour, IUnityPlanetVisualizer
{

    [Inject] private readonly IConstants mConstants;

    private List<GameObject> mPlanets = new List<GameObject>();

    private void Awake()
    {
        var child = transform.GetChild(0).gameObject;
        child.SetActive(false);
        mPlanets.Add(child);
        for (int i = 1; i < mConstants.PlanetsToVisualize; ++i)
        {
            var planet = Instantiate(child, transform);
            mPlanets.Add(planet);
        }
        
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
            mPlanets[i].SetActive(false);
        }

        for(int i= 0; i < planets.Count; ++i)
        {
            mPlanets[i].SetActive(true);
            var oldPos = mPlanets[i].transform.position;
            oldPos.x = planets[i].X;
            oldPos.y = planets[i].Y;
            mPlanets[i].transform.position = oldPos;
        }
    }
}