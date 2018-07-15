using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;
using Planets;
using QuadTree;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UnityQuadTreeManager : MonoBehaviour, IBordersChangeListener
{
    [NotNull][Inject] private readonly ICamera mCamera;
    [NotNull][Inject] private readonly StartNodeCreator mStartNodeCreator;
    private IQuadTreeNode mRootNode;
    [NotNull][Inject] private readonly IUnityPlanetVisualizer mUnityPlanetVisualizer;
    public Text mLoadingProgressText;
    public GameObject mTextHolder;
    public Text mLoadingThreads;
    public Text mError;

    [NotNull]private readonly List<PlanetData> mPlanetData = new List<PlanetData>(25);
    // Use this for initialization
    void Awake()
    {
        mCamera.AddBorderChangeListener(this);
        mRootNode = mStartNodeCreator.Create();
        mLoadingThreads.text = Environment.ProcessorCount.ToString();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mLoadingProgressText.text = mStartNodeCreator.mProgress.ToString();
	    mTextHolder.SetActive(mStartNodeCreator.mIsWorking);
	    if (mStartNodeCreator.mError != null)
	    {
	        mError.text = mStartNodeCreator.mError;
            mTextHolder.SetActive(true);
	    }
	}

    public void NewBorders(int top, int bottom, int left, int right)
    {
        mPlanetData.Clear();
        mRootNode.GetVisiblePlanets(mCamera, mPlanetData);
        mUnityPlanetVisualizer.Visualize(mPlanetData);
    }
}
