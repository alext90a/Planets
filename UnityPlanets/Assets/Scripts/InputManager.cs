using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Planets;
using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour
{
    [Inject]
    private IPlayer mPlayer;

    [Inject] private IUnityPlayer mUnityPlayer;

    [Inject] private ICamera mCamera;
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
	}
}
