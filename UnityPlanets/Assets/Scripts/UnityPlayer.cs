using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Planets;
using UnityEngine;
using Zenject;
using Camera = UnityEngine.Camera;

public class UnityPlayer : MonoBehaviour, IUnityPlayer, ICameraListener
{
    [Inject] private ICamera mCamera;
    [Inject] private IConstants mConstants;
    private readonly Camera mUnityCamera;
    private SpriteRenderer mSpriteRenderer;
    private Vector3 mSpriteStartScale;

    void Awake()
    {
        mSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        mSpriteStartScale = mSpriteRenderer.transform.localScale;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PositionCanged(int posX, int posY)
    {
        var oldPosition = transform.position;
        oldPosition.x = posX + 0.5f;
        oldPosition.y = posY + 0.5f;

        transform.position = oldPosition;
    }

    public void ZoomValueChanged(int zoomValue)
    {
        mUnityCamera.orthographicSize = ((float)zoomValue) / 2f;
        var spriteScale = mSpriteRenderer.gameObject.transform.localScale;
        spriteScale = mSpriteStartScale * (float)zoomValue / (float)mConstants.MinCameraSize;
        mSpriteRenderer.transform.localScale = spriteScale;
    }
}
