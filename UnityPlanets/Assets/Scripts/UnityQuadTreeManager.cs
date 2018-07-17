using System;
using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using QuadTree;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UnityQuadTreeManager : MonoBehaviour, IBordersChangeListener, IArrayBackgroundWorkerListener,IZoomBlocker
{
    [NotNull][Inject]
    private readonly ICamera mCamera;
    [NotNull][Inject]
    private readonly IStartNodeCreator mStartNodeCreator;
    [NotNull]
    private IQuadTreeNode mRootNode;
    [NotNull][Inject]
    private readonly IUnityPlanetVisualizer mUnityPlanetVisualizer;
    [NotNull] [Inject]
    private readonly IStartUpNodeInitializer mStartupNodeInitializer;
    [NotNull] [Inject]
    private readonly IArrayBackgroundWorker mBackgroundWorker;
    [NotNull] [Inject]
    private readonly List<IZoomBlockerListener> mZoomBlockerListeners = new List<IZoomBlockerListener>();
    [NotNull] [Inject]
    private readonly IPlayer mPlayer;
    [NotNull] [Inject]
    private readonly IConstants mConstants;
    [NotNull]
    public Text mLoadingProgressText;
    [NotNull]
    public GameObject mTextHolder;
    [NotNull]
    public Text mLoadingThreads;
    [NotNull]
    private List<PlanetData> mPlanetData;

    // Use this for initialization
    void Start()
    {
        mPlanetData = new List<PlanetData>(mConstants.GetPlanetsToVisualize());
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

    public void AddListener(IZoomBlockerListener listener)
    {
        mZoomBlockerListeners.Add(listener);
    }

    public void BlockZoom()
    {
        foreach (var curListener in mZoomBlockerListeners)
        {
            // ReSharper disable once PossibleNullReferenceException
            curListener.OnZoomBlocked();
        }
    }

    public void UnblockZoom()
    {
        foreach (var curListener in mZoomBlockerListeners)
        {
            // ReSharper disable once PossibleNullReferenceException
            curListener.OnZoomUnblocked();
        }
    }
}
