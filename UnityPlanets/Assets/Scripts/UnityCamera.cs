using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public class UnityCamera : MonoBehaviour, ICameraListener
{
    [Inject][NotNull]
    private ICamera mPlanetCamera;
    [NotNull]
    private UnityEngine.Camera mCamera;


    void Awake()
    {
        mCamera = GetComponent<UnityEngine.Camera>();
        mPlanetCamera.AddListener(this);
    }

    public void ZoomValueChanged(int zoomValue)
    {
        mCamera.orthographicSize = (float) zoomValue / 2f;
    }
}
