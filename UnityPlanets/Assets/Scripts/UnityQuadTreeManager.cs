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

public class UnityQuadTreeManager : MonoBehaviour, IBordersChangeListener, IArrayBackgroundWorkerListener
{
    [NotNull][Inject] private readonly ICamera mCamera;
    [NotNull][Inject] private readonly StartNodeCreator mStartNodeCreator;
    private IQuadTreeNode mRootNode;
    [NotNull][Inject] private readonly IUnityPlanetVisualizer mUnityPlanetVisualizer;
    [NotNull] [Inject] private readonly StartUpNodeInitializer mStartupNodeInitializer;
    [NotNull] [Inject] private readonly IArrayBackgroundWorker mBackgroundWorker;
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
        mBackgroundWorker.AddListener(this);
        mLoadingThreads.text = Environment.ProcessorCount.ToString();
        mStartupNodeInitializer.Run(mCamera, mRootNode);
    }

    public void NewBorders(int top, int bottom, int left, int right)
    {
        mPlanetData.Clear();
        mRootNode.GetVisiblePlanets(mCamera, mPlanetData);
        mUnityPlanetVisualizer.Visualize(mPlanetData);
    }

    public void OnProgressChange(int progress)
    {
        mLoadingProgressText.text = progress.ToString();
    }

    public void OnFinished()
    {
        mTextHolder.SetActive(false);
    }

    public void OnException(string message)
    {

        mError.text = message;
        mTextHolder.SetActive(true);

    }
}
