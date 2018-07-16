﻿using JetBrains.Annotations;

public interface IArrayBackgroundWorkerListener
{
    void OnProgressChange(int progress);
    void OnFinished();
    void OnException([NotNull] string message);
}