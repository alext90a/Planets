using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Planets;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerDataController : MonoBehaviour, IPlayerListener
{
    [SerializeField] private Text mTextX;
    [SerializeField] private Text mTextY;
    [SerializeField] private Text mScore;
    [SerializeField] private Text mZoomText;
    [SerializeField] private Text mZoomText2;

    [Inject] private readonly IPlayer mPlayer;
	// Use this for initialization
	void Start () {
		mPlayer.AddListener(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PositionCanged(int posX, int posY)
    {
        mTextX.text = posX.ToString();
        mTextY.text = posY.ToString();
    }
}
