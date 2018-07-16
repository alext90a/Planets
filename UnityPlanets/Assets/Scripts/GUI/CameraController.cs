using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Assets.Scripts;
using JetBrains.Annotations;
using Planets;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraController : MonoBehaviour, ICameraListener, IZoomBlockerListener
{

    [SerializeField] [NotNull] private GameObject mZoomControlHolder;
    [SerializeField] [NotNull] private Text mZoomText;
    [SerializeField] [NotNull] private Text mZoomText2;
    [SerializeField] [NotNull] private Slider mSlider;
    [SerializeField] [NotNull] private Button mIncreaseButton;
    [SerializeField] [NotNull] private Button mDecreaseButton;
    
    

    [Inject][NotNull] private readonly ICamera mCamera;
    [Inject][NotNull] private readonly IZoomBlocker mZoomBlocker;

    void Awake()
    {
        mZoomBlocker.AddListener(this);
    }

    // Use this for initialization
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    void Start () {
		mCamera.AddListener(this);
        mSlider.maxValue = mCamera.GetMaxZoom();
        mSlider.minValue = mCamera.GetMinZoom();
        mIncreaseButton.onClick.AddListener(IncreaseZoom);
        mDecreaseButton.onClick.AddListener(DecreaseZoom);
        ZoomValueChanged(mCamera.GetZoom());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ZoomValueChanged(int zoomValue)
    {
        mZoomText.text = zoomValue.ToString();
        mZoomText2.text = zoomValue.ToString();
        mSlider.value = zoomValue;

        
    }

    private void IncreaseZoom()
    {
        mCamera.IncreaseZoom();
    }

    private void DecreaseZoom()
    {
        mCamera.DecreaseZoom();
    }

    public void OnZoomBlocked()
    {
        mZoomControlHolder.SetActive(false);
    }

    public void OnZoomUnblocked()
    {
        mZoomControlHolder.SetActive(true);
    }
}
