using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ExitController : MonoBehaviour
{
    [NotNull][SerializeField]
    private Button mExitButton;

	// Use this for initialization
	void Start ()
	{
	    mExitButton.onClick.AddListener(Exit);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Exit()
    {
        Application.Quit();
    }
}
