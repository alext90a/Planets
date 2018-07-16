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

public class UnityQuadTreeManager : MonoBehaviour, IBordersChangeListener, IArrayBackgroundWorkerListener,IZoomBlocker
{
    [NotNull][Inject] private readonly ICamera mCamera;
    [NotNull][Inject] private readonly StartNodeCreator mStartNodeCreator;
    private IQuadTreeNode mRootNode;
    [NotNull][Inject] private readonly IUnityPlanetVisualizer mUnityPlanetVisualizer;
    [NotNull] [Inject] private readonly StartUpNodeInitializer mStartupNodeInitializer;
    [NotNull] [Inject] private readonly IArrayBackgroundWorker mBackgroundWorker;
    [NotNull] [Inject] private readonly List<IZoomBlockerListener> mZoomBlockerListeners = new List<IZoomBlockerListener>();
    [NotNull] [Inject] private readonly IPlayer mPlayer;
    [NotNull] [Inject] private readonly IConstants mConstants;
    public Text mLoadingProgressText;
    public GameObject mTextHolder;
    public Text mLoadingThreads;
    public Text mError;

    [NotNull]private readonly List<PlanetData> mPlanetData = new List<PlanetData>(25);
    // Use this for initialization
    void Start()
    {
        BlockZoom();
        mCamera.AddBorderChangeListener(this);
        mRootNode = mStartNodeCreator.Create();
        mLoadingThreads.text = Environment.ProcessorCount.ToString();
        mStartupNodeInitializer.Run(mCamera, mRootNode, this);
        mPlayer.MoveRight();
    }

    public void NewBorders(int top, int bottom, int left, int right)
    {
        mPlanetData.Clear();
        //mRootNode.GetVisiblePlanets(mCamera, mPlanetData);
        //mUnityPlanetVisualizer.Visualize(mPlanetData);
        var visualizationVisitor = new VisualizationPlanetVisitor(mPlayer, mConstants, mCamera);
        mRootNode.VisitVisibleNodes(mCamera, visualizationVisitor);
        mUnityPlanetVisualizer.Visualize(visualizationVisitor.GetVisiblePlanets());
    }

    public void OnProgressChange(int progress)
    {
        mLoadingProgressText.text = progress.ToString();
    }

    public void OnFinished()
    {
        mTextHolder.SetActive(false);
        UnblockZoom();
    }

    public void OnException(string message)
    {

        mError.text = message;
        mTextHolder.SetActive(true);

    }

    public void AddListener(IZoomBlockerListener listener)
    {
        mZoomBlockerListeners.Add(listener);
    }

    public void BlockZoom()
    {
        foreach (var curListener in mZoomBlockerListeners)
        {
            curListener.OnZoomBlocked();
        }
    }

    public void UnblockZoom()
    {
        foreach (var curListener in mZoomBlockerListeners)
        {
            curListener.OnZoomUnblocked();
        }
    }
}
