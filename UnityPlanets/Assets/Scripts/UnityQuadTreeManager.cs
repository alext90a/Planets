using System.Collections.Generic;
using Assets.Scripts;
using JetBrains.Annotations;
using QuadTree;
using UnityEngine;
using Zenject;

public class UnityQuadTreeManager : MonoBehaviour, IBordersChangeListener, IArrayBackgroundWorkerListener,IZoomBlocker, IRootNodeProvider
{
    [NotNull][Inject]
    private readonly ICamera mCamera;
    [NotNull][Inject]
    private readonly IStartNodeCreator mStartNodeCreator;
    [NotNull]
    private IQuadTreeNode mRootNode;
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
    

    // Use this for initialization
    void Start()
    {
        
        BlockZoom();
        mCamera.AddBorderChangeListener(this);
        mRootNode = mStartNodeCreator.Create();
        
        mStartupNodeInitializer.Run(mCamera, mRootNode, this);
        mPlayer.MoveRight();
    }

    public void NewBorders(int top, int bottom, int left, int right)
    {
        
    }

    public void OnProgressChange(int progress)
    {
        
    }

    public void OnFinished()
    {
        
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

    public IQuadTreeNode GetRootNote()
    {
        return mRootNode;
    }
}
