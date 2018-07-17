using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ExceptionController : MonoBehaviour {

    [NotNull]
    public Text mError;

    // Use this for initialization
    void Start () {

        Application.logMessageReceived += ApplicationOnLogMessageReceived;
    }

    private void ApplicationOnLogMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            // ReSharper disable once PossibleNullReferenceException
            mError.gameObject.SetActive(true);
            mError.text = condition + stackTrace;
        }
    }
}
