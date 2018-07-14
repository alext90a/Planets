using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class UnityPlanetData : MonoBehaviour, IUnityPlanetData
{
    [SerializeField][NotNull] private Text mText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate(PlanetData planetData)
    {
        transform.position = new Vector3(planetData.X + 0.5f, planetData.Y + 0.5f);
        mText.text = planetData.Score.ToString();
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
