using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using Debug = System.Diagnostics.Debug;

public class UnityCameraSprite : MonoBehaviour, ICameraListener
{
    [Inject][NotNull]
    private readonly IConstants mConstants;
    [Inject][NotNull]
    private readonly ICamera mCamera;
    [NotNull]
    private SpriteRenderer mSpriteRenderer;
    private Vector3 mSpriteStartScale;

    void Awake()
    {
        
        mSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Debug.Assert(mSpriteRenderer != null, "mSpriteRenderer != null");
        if (mSpriteRenderer.transform != null)
        {
            mSpriteStartScale = mSpriteRenderer.transform.localScale;
        }
        mCamera.AddListener(this);
    }

    public void ZoomValueChanged(int zoomValue)
    {
        var spriteScale = mSpriteStartScale * zoomValue / mConstants.GetMinCameraSize();
        // ReSharper disable once PossibleNullReferenceException
        mSpriteRenderer.transform.localScale = spriteScale;
    }
}
