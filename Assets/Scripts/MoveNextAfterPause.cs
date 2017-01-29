using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNextAfterPause : MonoBehaviour {
    public  float pauseInSeconds;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.timeSinceLevelLoad > pauseInSeconds)
            Application.LoadLevel(Application.loadedLevel + 1);
	}
}
