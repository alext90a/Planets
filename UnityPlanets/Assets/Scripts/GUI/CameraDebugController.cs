using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Planets;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraDebugController : MonoBehaviour, IBordersChangeListener
{

    [SerializeField] private Text mCameraTop;
    [SerializeField] private Text mCameraBottom;
    [SerializeField] private Text mCameraLeft;
    [SerializeField] private Text mCameraRight;

    [Inject] private readonly ICamera mCamera;

    // Use this for initialization
    void Start () {
		mCamera.AddBorderChangeListener(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewBorders(int top, int bottom, int left, int right)
    {
        mCameraTop.text = top.ToString();
        mCameraBottom.text = bottom.ToString();
        mCameraLeft.text = left.ToString();
        mCameraRight.text = right.ToString();
    }
}
