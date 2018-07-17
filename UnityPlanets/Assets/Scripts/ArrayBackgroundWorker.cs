using System;
using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts;
using JetBrains.Annotations;
using QuadTree;

// ReSharper disable PossibleNullReferenceException

public sealed class ArrayBackgroundWorker : IArrayBackgroundWorker
{
    private BackgroundWorker[] mBackgroundWorkers;
    private string mErrorMessage;
    private int mProgress = 0;
    [NotNull]
    private readonly List<IArrayBackgroundWorkerListener> mListeners = new List<IArrayBackgroundWorkerListener>();
        
    public void Run(IReadOnlyList<QuadTreeLeaf> loadingNodes, IPlanetFactoryCreator planetFactoryCreator)
    {
        mErrorMessage = null;
        var processorCount = Environment.ProcessorCount;
        var leafsPerProc = loadingNodes.Count / processorCount;
        mBackgroundWorkers = new BackgroundWorker[processorCount];
        //mIsWorking = true;
        for (int j = 0; j < processorCount; ++j)
        {
            var startIndex = j * leafsPerProc;
            var endIndex = startIndex + leafsPerProc;
            if (endIndex >= loadingNodes.Count)
            {
                endIndex = loadingNodes.Count - 1;
            }
            BackgroundWorker worker = new BackgroundWorker();
            mBackgroundWorkers[j] = worker;
            var planetFactory = planetFactoryCreator.CreatePlanetFactory();
            worker.DoWork +=  (sender, args)=>
            {
                var totalLeafs = endIndex - startIndex;
                for (int i = startIndex; i < endIndex; ++i)
                {

                    if (loadingNodes[i].IsPlanetDataNull())
                    {
                        loadingNodes[i].SetPlanets(planetFactory.CreatePlanetsForSector());
                    }
                        
                    var floatData = ((float)(i - startIndex) / totalLeafs) * 100f;

                    worker.ReportProgress((int)floatData);
                }
                

            };
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (sender, args) => { CallBackFromLoad(sender, args); };
            worker.RunWorkerAsync();
            worker.RunWorkerCompleted += WorkerOnRunWorkerCompleted;

        }
    }

    private void WorkerOnRunWorkerCompleted(object o, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
    {
        if (runWorkerCompletedEventArgs.Error != null)
        {
            mErrorMessage = runWorkerCompletedEventArgs.Error.Message;
        }
        foreach (var worker in mBackgroundWorkers)
        {
            if (worker.IsBusy)
            {
                return;
            }
        }
        foreach (var curListener in mListeners)
        {
            if (mErrorMessage != null)
            {
                throw new Exception(mErrorMessage);
            }
            else
            {
                curListener.OnFinished();
            }
        }
    }

    private void CallBackFromLoad(object sender, ProgressChangedEventArgs args)
    {
        if (mProgress > args.ProgressPercentage)
        {
            return;
        }
        mProgress = args.ProgressPercentage;
        foreach (var curListener in mListeners)
        {
            curListener.OnProgressChange(mProgress);
        }
    }

    public void AddListener(IArrayBackgroundWorkerListener listener)
    {
        mListeners.Add(listener);
    }

}