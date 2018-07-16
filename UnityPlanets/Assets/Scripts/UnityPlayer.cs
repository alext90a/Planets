using UnityEngine;
using Zenject;

public sealed class UnityPlayer : MonoBehaviour, IUnityPlayer
{
    [Inject] private ICamera mCamera;
    [Inject] private IConstants mConstants;
    private readonly UnityEngine.Camera mUnityCamera;

    public void PositionCanged(int posX, int posY)
    {
        // ReSharper disable once PossibleNullReferenceException
        var oldPosition = transform.position;
        oldPosition.x = posX + 0.5f;
        oldPosition.y = posY + 0.5f;

        transform.position = oldPosition;
    }

    
}
