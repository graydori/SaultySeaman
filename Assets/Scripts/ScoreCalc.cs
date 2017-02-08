using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalc : MonoBehaviour {

    public string continueText;

    public AudioClip[] poorScore;
    public AudioClip[] okScore;
    public AudioClip[] goodScore;
    
	// Use this for initialization
	void Start () {
        int total = UserState.success + UserState.misses;
        float score = 40f;
        if (total > 0) score = (float)UserState.success / total * 100;

        GetComponent<Text>().text = "seduction score: " + Mathf.Round(score) + "% \n\n " + continueText;

        AudioClip[] clipArray;
        if (score <= 33)
            clipArray = poorScore;
        else if (score <= 75)
            clipArray = okScore;
        else
            clipArray = goodScore;
        AudioClip clip = clipArray[Random.Range(0, clipArray.Length)];
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();

        // Clear for replay
        UserState.success = 0;
        UserState.misses = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
