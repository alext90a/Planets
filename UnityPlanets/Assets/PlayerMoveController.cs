using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Planets;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
// ReSharper disable NotNullMemberIsNotInitialized

public class PlayerMoveController : MonoBehaviour {

    [NotNull]
    [SerializeField] private Button mUpButton;
    [NotNull]
    [SerializeField] private Button mDownButton;
    [NotNull]
    [SerializeField] private Button mLeftButton;
    [NotNull]
    [SerializeField] private Button mRightButton;

    [NotNull]
    [Inject] private readonly IPlayer mPlayer;

    // Use this for initialization
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    private void Start ()
    {
	    mUpButton.onClick.AddListener(UpClicked);	
        mDownButton.onClick.AddListener(DownClicked);
        mLeftButton.onClick.AddListener(LeftClicked);
        mRightButton.onClick.AddListener(RightClicked);
	}

    void UpClicked()
    {
        mPlayer.MoveTop();
    }

    void DownClicked()
    {
        mPlayer.MoveBottom();
    }

    void LeftClicked()
    {
        mPlayer.MoveLeft();
    }

    void RightClicked()
    {
        mPlayer.MoveRight();
    }
	
}
