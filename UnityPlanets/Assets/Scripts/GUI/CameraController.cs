using System.Collections;
using System.Collections.Generic;
using Planets;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraController : MonoBehaviour, ICameraListener {

    [SerializeField] private Text mZoomText;
    [SerializeField] private Text mZoomText2;
    [SerializeField] private Slider mSlider;
    [SerializeField] private Button mIncreaseButton;
    [SerializeField] private Button mDecreaseButton;
    
    

    [Inject] private readonly ICamera mCamera;

    // Use this for initialization
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
}
