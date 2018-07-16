using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using Planets;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour, IZoomBlockerListener
{
    [Inject][NotNull]private IPlayer mPlayer;

    [Inject] [NotNull] private IUnityPlayer mUnityPlayer;
    [Inject] [NotNull] private readonly IZoomBlocker mZoomBlocker;

    [Inject] [NotNull] private ICamera mCamera;
    private bool mIsZoomBlocked = false;

    void Awake()
    {
        mZoomBlocker.AddListener(this);
    }

	// Use this for initialization
	void Start () {
		mPlayer.AddListener(mUnityPlayer);
        mPlayer.AddListener(mCamera);
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.A))
	    {
	        mPlayer.MoveLeft();
	    }
	    if (Input.GetKeyDown(KeyCode.S))
	    {
	        mPlayer.MoveBottom();
	    }
	    if (Input.GetKeyDown(KeyCode.D))
	    {
	        mPlayer.MoveRight();
	    }
	    if (Input.GetKeyDown(KeyCode.W))
	    {
	        mPlayer.MoveTop();
	    }
	    if (!mIsZoomBlocked)
	    {
	        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
	        {
	            mCamera.IncreaseZoom();
	        }

	        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
	        {
	            mCamera.DecreaseZoom();
	        }
        }
	    
    }

    public void OnZoomBlocked()
    {
        mIsZoomBlocked = true;
    }

    public void OnZoomUnblocked()
    {
        mIsZoomBlocked = false;
    }
}
