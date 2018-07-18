using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GUI
{
    public sealed class LoadingController : MonoBehaviour, IArrayBackgroundWorkerListener
    {
        [NotNull]
        public Text mLoadingProgressText;
        [NotNull]
        public GameObject mTextHolder;
        [NotNull]
        public Text mLoadingThreads;

        [NotNull]
        [Inject]
        private readonly IArrayBackgroundWorker mBackgroundWorker;

        private void Start()
        {
            mLoadingThreads.text = Environment.ProcessorCount.ToString();
            mBackgroundWorker.AddListener(this);
        }

        public void OnProgressChange(int progress)
        {
            mLoadingProgressText.text = progress.ToString();
        }

        public void OnFinished()
        {
            mTextHolder.SetActive(false);
        }
    }
}
