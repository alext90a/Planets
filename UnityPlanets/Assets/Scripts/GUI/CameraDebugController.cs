using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using Planets;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraDebugController : MonoBehaviour, IBordersChangeListener
{

    [SerializeField] [NotNull] private Text mCameraTop;
    [SerializeField] [NotNull] private Text mCameraBottom;
    [SerializeField] [NotNull] private Text mCameraLeft;
    [SerializeField] [NotNull] private Text mCameraRight;

    [Inject][NotNull] private readonly ICamera mCamera;

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
