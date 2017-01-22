﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalc : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int total = UserState.success + UserState.misses;
        float score = 40f;
        if (total > 0) score = (float)UserState.success / total * 100;
        
        GetComponent<Text>().text = "seduction score: " + Mathf.Round( score ) + "% \n\n SING TO REPLAY";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
