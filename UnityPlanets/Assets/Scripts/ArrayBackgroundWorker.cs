using System;
using System.Collections.Generic;
using System.ComponentModel;
using Assets.Scripts.QuadTree;
using JetBrains.Annotations;
// ReSharper disable PossibleNullReferenceException

namespace Assets.Scripts
{
    public class ArrayBackgroundWorker : IArrayBackgroundWorker
    {
        private BackgroundWorker[] mBackgroundWorkers;
        //private int mCompletedWorkers = 0;
        //public bool mIsWorking = false;
        private string mError;

        private int mProgress = 0;
        [NotNull]
        private readonly List<IArrayBackgroundWorkerListener> mListeners = new List<IArrayBackgroundWorkerListener>();
        
        public void Run(IReadOnlyList<QuadTreeLeaf> mLoadingNodes, IPlanetFactoryCreator planetFactoryCreator)
        {
            mError = null;
            var processorCount = Environment.ProcessorCount;
            var leafsPerProc = mLoadingNodes.Count / processorCount;
            mBackgroundWorkers = new BackgroundWorker[processorCount];
            //mIsWorking = true;
            for (int j = 0; j < processorCount; ++j)
            {
                var startIndex = j * leafsPerProc;
                var endIndex = startIndex + leafsPerProc;
                if (endIndex >= mLoadingNodes.Count)
                {
                    endIndex = mLoadingNodes.Count - 1;
                }
                BackgroundWorker worker = new BackgroundWorker();
                mBackgroundWorkers[j] = worker;
                var planetFactory = planetFactoryCreator.CreatePlanetFactory();
                worker.DoWork +=  (sender, args)=>
                {
                    var totalLeafs = endIndex - startIndex;
                    for (int i = startIndex; i < endIndex; ++i)
                    {

                        if (mLoadingNodes[i].IsPlanetDataNull())
                        {
                            mLoadingNodes[i].SetPlanets(planetFactory.CreatePlanetsForSector());
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
                mError = runWorkerCompletedEventArgs.Error.Message;
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
                if (mError != null)
                {
                    curListener.OnException(mError);
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
}
