using System.Collections;
using System.Collections.Generic;
using Planets;
using UnityEngine;
using Zenject;

public class UnityCameraSprite : MonoBehaviour, ICameraListener
{
    [Inject] private IConstants mConstants;
    [Inject] private ICamera mCamera;
    private SpriteRenderer mSpriteRenderer;
    private Vector3 mSpriteStartScale;

    void Awake()
    {
        
        mSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        mSpriteStartScale = mSpriteRenderer.transform.localScale;
        mCamera.AddListener(this);
    }
	// Use this for initialization
	void Start () {
	    
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ZoomValueChanged(int zoomValue)
    {
        if (name == "Planet01")
        {
            int i = 9;
            ++i;
        }
        var spriteScale = mSpriteStartScale * (float)zoomValue / (float)mConstants.MinCameraSize;
        mSpriteRenderer.transform.localScale = spriteScale;
    }
}
