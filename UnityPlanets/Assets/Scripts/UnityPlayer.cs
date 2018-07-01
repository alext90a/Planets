using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class UnityPlayer : MonoBehaviour, IUnityPlayer {

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void PositionCanged(int posX, int posY)
    {
        var oldPosition = transform.position;
        oldPosition.x = posX + 0.5f;
        oldPosition.y = posY + 0.5f;

        transform.position = oldPosition;
    }
}
