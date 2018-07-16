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
	    mExitButton.onClick?.AddListener(Exit);
	}

    void Exit()
    {
        Application.Quit();
    }
}
