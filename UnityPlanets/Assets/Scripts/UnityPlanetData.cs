using System.Collections;
using System.Collections.Generic;
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

    public void Activate(int xPos, int yPos, int score)
    {
        transform.position = new Vector3(xPos + 0.5f, yPos + 0.5f);
        mText.text = score.ToString();
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
