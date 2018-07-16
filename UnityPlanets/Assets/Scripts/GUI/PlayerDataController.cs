using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using Planets;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerDataController : MonoBehaviour, IPlayerListener
{
    [SerializeField] [NotNull] private Text mTextX;
    [SerializeField] [NotNull] private Text mTextY;
    [SerializeField] [NotNull] private Text mScore;
    

    [Inject] [NotNull] private readonly IPlayer mPlayer;
	// Use this for initialization
	void Start () {
		mPlayer.AddListener(this);
	    mScore.text = mPlayer.Score.ToString();
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
