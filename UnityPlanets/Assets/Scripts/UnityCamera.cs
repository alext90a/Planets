using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using Planets;
using QuadTree;
using UnityEngine;
using Zenject;
using Camera = UnityEngine.Camera;

public class UnityCamera : MonoBehaviour, ICameraListener
{
    [Inject] private ICamera mPlanetCamera;

    private Camera mCamera;


    void Awake()
    {
        mCamera = GetComponent<Camera>();
        mPlanetCamera.AddListener(this);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ZoomValueChanged(int zoomValue)
    {
        mCamera.orthographicSize = (float) zoomValue / 2f;
    }
}
