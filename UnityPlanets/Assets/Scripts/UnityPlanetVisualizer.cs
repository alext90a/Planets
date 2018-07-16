﻿using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public class UnityPlanetVisualizer : MonoBehaviour, IUnityPlanetVisualizer
{
    [Inject][NotNull]
    private readonly IConstants mConstants;
    [Inject]
    [NotNull]
    private readonly ICamera mCamera;
    [Inject][NotNull]
    private readonly UnityPlanetDataFactory mPlanetDataFactory;
    [NotNull]
    private List<IUnityPlanetData> mPlanets;
    
    private void Awake()
    {
        mPlanets = new List<IUnityPlanetData>(mConstants.GetPlanetsToVisualize());
        if (transform != null)
        {
            var chilObject = transform.GetChild(0);
            if (chilObject != null)
            {
                var child = chilObject.gameObject;
                for (int i = 0; i < mConstants.GetPlanetsToVisualize(); ++i)
                {
                    var planet = mPlanetDataFactory.Create().gameObject;
                    if (planet != null)
                    {
                        if (planet.transform != null)
                        {
                            planet.transform.parent = transform;
                        }
                        planet.SetActive(false);
                        mPlanets.Add(planet.GetComponent<IUnityPlanetData>());
                    }
                }
                if (child != null)
                {
                    child.SetActive(false);
                }
            }
        }
    }

    public void Visualize(List<PlanetData> planets)
    {
        for (int i = 0; i < mPlanets.Count; ++i)
        {
            // ReSharper disable once PossibleNullReferenceException
            mPlanets[i].Deactivate();
        }

        for(int i= 0; i < planets.Count; ++i)
        {
            // ReSharper disable once PossibleNullReferenceException
            mPlanets[i].Activate(planets[i]);
        }
    }
}
