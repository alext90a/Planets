using Assets.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UnityPlanetData : MonoBehaviour, IUnityPlanetData
{
    [SerializeField][NotNull]
    private Text mText;

    public void Activate(PlanetData planetData)
    {
        // ReSharper disable once PossibleNullReferenceException
        transform.position = new Vector3(planetData.X + 0.5f, planetData.Y + 0.5f);
        mText.text = planetData.Score.ToString();
        // ReSharper disable once PossibleNullReferenceException
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        // ReSharper disable once PossibleNullReferenceException
        gameObject.SetActive(false);
    }
}
